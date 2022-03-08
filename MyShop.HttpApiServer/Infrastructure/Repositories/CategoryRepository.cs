using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.HttpApiServer.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Category> GetAll() => _context.Categories;

    public Task<Category> GetById(Guid id) => _context.Categories.FirstAsync(category => category.Id == id);

    public Task<Category?> FindById(Guid id) => _context.Categories.FirstOrDefaultAsync(category => category.Id == id);

    public async Task Add(Category item) => await _context.Categories.AddAsync(item);
    
    public void Update(Category item) => _context.Entry(item).State = EntityState.Modified;

    public void Delete(Category item) => _context.Remove(item);

    public Task Save() => _context.SaveChangesAsync();
}