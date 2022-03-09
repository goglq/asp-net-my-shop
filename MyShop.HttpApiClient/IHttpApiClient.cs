using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiClient;

public interface IHttpApiClient
{
    Task<IReadOnlyList<Product>?> GetAll(int skip = 0, int take = 10);

    Task<Product?> Get(Guid id);

    Task Create(ProductDto product);
}