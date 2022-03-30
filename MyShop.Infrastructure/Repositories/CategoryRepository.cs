using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Models;
using MyShop.Core;
using MyShop.Infrastructure.Databases;

namespace MyShop.Infrastructure.Repositories;

public class CategoryRepository : EfRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }
}