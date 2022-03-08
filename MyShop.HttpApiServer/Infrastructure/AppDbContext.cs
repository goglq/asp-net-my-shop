using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.HttpApiServer.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}