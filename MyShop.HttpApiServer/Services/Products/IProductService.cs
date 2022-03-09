using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Services.Products;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAll(int skip, int take);

    Task<Product> Get(Guid id);

    Task Create(ProductDto productDto);
}