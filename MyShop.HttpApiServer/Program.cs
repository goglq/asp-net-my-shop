using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyShop.Core.Interfaces;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Services;
using MyShop.Core.Models;
using MyShop.Core.Options;
using MyShop.Core.Services;
using MyShop.HttpApiServer.Filters;
using MyShop.HttpApiServer.Middlewares;
using MyShop.Infrastructure.Databases;
using MyShop.Infrastructure.Repositories;
using MyShop.Infrastructure.UnitOfWork;
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

    var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();

    var corsSettings = builder.Configuration.GetSection("CorsSettings").Get<CorsSettings>();

    builder.Services.AddSingleton(jwtConfig);
    
    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudiences = new [] { jwtConfig.Audience },
                ValidIssuer = jwtConfig.Issuer
            };
        });
    builder.Services.AddAuthorization();
    
    builder.Services.AddCors();
    builder.Services
        .AddControllers(options =>
        {
            options.Filters.Add<MyValidationFilter>();
            options.Filters.Add<MyShopExceptionFilter>();
        })
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        })
        .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo()
        {
            Version = "v1",
            Title = "My Shop App",
            Description = "An ASP.NET Core Web API for e-commerce"
        });
    });

    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

    builder.Services.Configure<ProductServiceOptions>(builder.Configuration.GetSection("ProductServiceOptions"));

    builder.Services.AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>();

    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<ICartRepository, CartRepository>();
    builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
    builder.Services.AddScoped<IConfirmationCodeRepository, ConfirmationCodeRepository>();

    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IProductService, ProductService>();      
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<ICartService, CartService>();
    builder.Services.AddScoped<ITwoFactorService, TwoFactorService>();

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    var app = builder.Build();
    Log.Information("Building is complete!");

    //app.UseMiddleware<EdgeGuardMiddleware>();
    
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
            .WithOrigins(corsSettings.Origins.ToArray())
            .AllowAnyHeader()
            .AllowAnyMethod());
    
    app.UseAuthentication();
    app.UseAuthorization();

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

