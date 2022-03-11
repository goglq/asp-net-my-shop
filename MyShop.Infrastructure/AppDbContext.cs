using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>()
            .HasIndex(account => account.Email)
            .IsUnique();
    }
}