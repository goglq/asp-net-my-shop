using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MyShop.HttpApiServer.Infrastructure;
using MyShop.HttpApiServer.Infrastructure.Repositories;
using MyShop.HttpApiServer.Services.Categories;
using MyShop.HttpApiServer.Services.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=myapp.db"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

app.UseCors(policy => 
    policy
        .WithOrigins("https://localhost:7074")
        .AllowAnyHeader()
        .AllowAnyMethod());

app.MapControllers();
app.Run();