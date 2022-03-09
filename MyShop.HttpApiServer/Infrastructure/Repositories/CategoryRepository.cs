using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.HttpApiServer.Infrastructure.Repositories;

public class CategoryRepository : EfRepository<Category>, ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context) : base(context) { }
}