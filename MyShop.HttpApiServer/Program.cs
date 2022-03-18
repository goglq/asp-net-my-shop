using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyShop.HttpApiServer.Middlewares;
using MyShop.Infrastructure;
using MyShop.Infrastructure.Options;
using MyShop.Infrastructure.Repositories;
using MyShop.Infrastructure.Services.Accounts;
using MyShop.Infrastructure.Services.Categories;
using MyShop.Infrastructure.Services.Products;
using MyShop.Models;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Building...");
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, config) => 
        config.ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddCors();
    builder.Services
        .AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo()
        {
            Version = "v1",
            Title = "My Shop App",
            Description = "An ASP.NET Core Web API for e-commerce",
            // TermsOfService = new Uri("https://example.com/terms"),
            // Contact = new OpenApiContact
            // {
            //     Name = "Example Contact",
            //     Url = new Uri("https://example.com/contact")
            // },
            // License = new OpenApiLicense
            // {
            //     Name = "Example License",
            //     Url = new Uri("https://example.com/license")
            // }
        });
    });

    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=myapp.db"));

    builder.Services.Configure<ProductServiceOptions>(builder.Configuration.GetSection("ProductServiceOptions"));

    builder.Services.AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>();

    builder.Services.AddScoped<HeaderLoggerMiddleware>();
    builder.Services.AddScoped<EdgeGuardMiddleware>();
    
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();

    builder.Services.AddScoped<IProductService, ProductService>();      
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IAccountService, AccountService>();

    var app = builder.Build();
    Log.Information("Building is complete!");

    app.UseMiddleware<EdgeGuardMiddleware>();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
    }
    
    app.UseMiddleware<HeaderLoggerMiddleware>();
    
    app.UseCors(policy =>
        policy
            .WithOrigins("https://localhost:7074")
            .AllowAnyHeader()
            .AllowAnyMethod());

    app.MapControllers();
    
    Log.Information("Server is running!");
    app.Run();
}
catch (Exception ex) when (ex.GetType().Name != "StopTheHostException")
{
    Log.Fatal(ex, @"Unhandled Exception {ExceptionType}", ex.GetType().Name);
}
finally
{
    Log.Information("Shutdown is complete!");
    Log.CloseAndFlush();
}