using MyShop.HttpApiServer.Infrastructure;
using MyShop.Models;

namespace MyShop.Infrastructure.Repositories;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext) { }
}