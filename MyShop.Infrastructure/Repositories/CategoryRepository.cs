using MyShop.HttpApiServer.Infrastructure;
using MyShop.Models;

namespace MyShop.Infrastructure.Repositories;

public class CategoryRepository : EfRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }
}