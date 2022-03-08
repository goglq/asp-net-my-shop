using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.HttpApiServer.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> GetAll() => _context.Products;

    public Task<Product> GetById(Guid id) => _context.Products.FirstAsync(product => product.Id == id);

    public Task<Product?> FindById(Guid id) => _context.Products.FirstOrDefaultAsync(product => product.Id == id);

    public async Task Add(Product item) => await _context.Products.AddAsync(item);

    public void Update(Product item) => _context.Entry(item).State = EntityState.Modified;

    public void Delete(Product item)=> _context.Products.Remove(item);

    public Task Save() => _context.SaveChangesAsync();
}