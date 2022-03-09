using MyShop.Models;

namespace MyShop.HttpApiServer.Infrastructure.Repositories;

public interface IRepository<T> where T : class, IEntity
{
    IQueryable<T> GetAll();

    Task<T> GetById(Guid id);

    Task<T?> FindById(Guid id);

    Task Add(T item);
    
    void Update(T item);

    void Delete(T item);

    Task Save();
}