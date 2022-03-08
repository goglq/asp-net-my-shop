using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiClient;

public interface IHttpApiClient
{
    Task<IReadOnlyList<Product>?> GetAll();

    Task<Product?> Get(Guid id);

    Task Create(ProductDto product);
}