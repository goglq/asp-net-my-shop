using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.HttpApiServer.Infrastructure.Repositories;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext) { }
}