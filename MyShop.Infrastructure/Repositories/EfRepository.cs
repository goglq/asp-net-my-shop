using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Infrastructure.Repositories;

public abstract class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly AppDbContext _dbContext;

    protected EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

    public IQueryable<TEntity> GetAll() => Entities;

    public Task<TEntity> GetById(Guid id) => Entities.FirstAsync(item => item.Id == id);

    public Task<TEntity?> FindById(Guid id) => Entities.FirstOrDefaultAsync(item => item.Id == id);

    public async Task Add(TEntity item) => await Entities.AddAsync(item);

    public void Update(TEntity item) => _dbContext.Entry(item).State = EntityState.Modified;

    public void Delete(TEntity item) => Entities.Remove(item);
    
    public Task Save() => _dbContext.SaveChangesAsync();
}