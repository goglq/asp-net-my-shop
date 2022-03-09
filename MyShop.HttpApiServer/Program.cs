using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MyShop.HttpApiServer.Infrastructure;
using MyShop.HttpApiServer.Infrastructure.Repositories;
using MyShop.HttpApiServer.Options;
using MyShop.HttpApiServer.Services.Categories;
using MyShop.HttpApiServer.Services.Products;
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

    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=myapp.db"));

    builder.Services.Configure<ProductServiceOptions>(builder.Configuration.GetSection("ProductServiceOptions"));

    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();

    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();

    var app = builder.Build();
    Log.Information("Building is complete!");

    app.UseCors(policy =>
        policy
            .WithOrigins("https://localhost:7074")
            .AllowAnyHeader()
            .AllowAnyMethod());

    app.MapControllers();
    
    Log.Information("Server is running!");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled Exception");
}
finally
{
    Log.Information("Shutdown is complete!");
    Log.CloseAndFlush();
}