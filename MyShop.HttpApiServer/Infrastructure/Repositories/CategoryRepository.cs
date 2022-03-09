using MyShop.Models;

namespace MyShop.HttpApiServer.Infrastructure.Repositories;

public class CategoryRepository : EfRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }
}