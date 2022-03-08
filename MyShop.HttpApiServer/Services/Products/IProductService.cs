using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Services.Products;

public interface IProductService
{
    Task Create(ProductDto productDto);
}