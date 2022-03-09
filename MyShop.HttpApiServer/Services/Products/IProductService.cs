using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Services.Products;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAll(int skip, int take, DayOfWeek dayOfWeek, string userAgent);

    Task<Product> Get(Guid id, DayOfWeek dayOfWeek, string userAgent);

    Task Create(ProductDto productDto);
}