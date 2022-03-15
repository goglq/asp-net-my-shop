using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiClient;

public interface IHttpApiClient
{
    Task<IReadOnlyList<Product>?> GetProducts(int skip = 0, int take = 10);

    Task<Product?> GetProduct(Guid id);

    Task<HttpResponseMessage> CreateProduct(ProductDto product);

    Task<HttpResponseMessage> RegisterAccount(AccountDto accountDto);
}