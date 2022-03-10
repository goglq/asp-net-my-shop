using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyShop.HttpApiServer.Options;
using MyShop.Infrastructure.Repositories;
using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Services.Products;

public class ProductService: IProductService
{
    private readonly ProductServiceOptions _options;
    
    private readonly IProductRepository _productRepository;

    public ProductService(IOptions<ProductServiceOptions> options, IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _options = options.Value;
    }

    //Функция приводит в InvalidOperationException при срабатывании в Select(product => ...) в функции ProductService.GetAll. Не понял почему.
    //Хотел использовать эту функцию, чтобы убрать повтор кода в функциях ProductService.GetAll и ProductService.Get.
    private Product GenerateModifiedProduct(Product product, DayOfWeek dayOfWeek, string userAgent)
    {
        var dayPercentage = dayOfWeek == DayOfWeek.Thursday ? _options.DailyOverpricePercentage : 0; 
        var devicePercentage = userAgent.Contains("Android") ? -_options.AndroidSalePercentage : _options.IPhoneOverpricePercentage;

        var modifierPercentage = dayPercentage + devicePercentage;

        return new Product()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price + product.Price * (modifierPercentage / 100m),
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId
        };
    }

    public async Task<IEnumerable<Product>> GetAll(int skip, int take, DayOfWeek dayOfWeek, string userAgent)
    {
        var dayPercentage = dayOfWeek == DayOfWeek.Thursday ? _options.DailyOverpricePercentage : 0; 
        var devicePercentage = userAgent.Contains("Android") ? -_options.AndroidSalePercentage : _options.IPhoneOverpricePercentage;

        var modifierPercentage = dayPercentage + devicePercentage;
        
        return await _productRepository
            .GetAll()
            .OrderBy(product => product.Name)
            .Skip(skip)
            .Take(take)
            .Select(product => new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price + product.Price * (modifierPercentage / 100m),
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId
            })
            .ToListAsync();
    }

    public async Task<Product> Get(Guid id, DayOfWeek dayOfWeek, string userAgent)
    {
        var dayPercentage = dayOfWeek == DayOfWeek.Thursday ? _options.DailyOverpricePercentage : 0; 
        var devicePercentage = userAgent.Contains("Android") ? -_options.AndroidSalePercentage : _options.IPhoneOverpricePercentage;

        var modifierPercentage = dayPercentage + devicePercentage;
        
        var product = await _productRepository.GetById(id);

        return new Product()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price + product.Price * (modifierPercentage / 100m),
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId
        };
    }

    public async Task Create(ProductDto productDto)
    {
        if (productDto.Price < 5)
            throw new ArgumentException("Product Price Cannot Be Less Than 5");
        
        var newProduct = new Product()
        {
            Id = Guid.NewGuid(),
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            CategoryId = Guid.Parse(productDto.CategoryId),
            ImageUrl = productDto.ImageUrl
        };
        await _productRepository.Add(newProduct);
        await _productRepository.Save();
    }
}