using Microsoft.EntityFrameworkCore;
using MyShop.Core.Models;

namespace MyShop.Infrastructure.Databases;

public sealed class AppDbContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Cart> Carts => Set<Cart>();

    public DbSet<CartItem> CartItems => Set<CartItem>();

    public DbSet<ConfirmationCode> ConfirmationCodes => Set<ConfirmationCode>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>()
            .HasIndex(account => account.Email)
            .IsUnique();
    }
}