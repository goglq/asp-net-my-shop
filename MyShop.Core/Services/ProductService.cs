using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyShop.Core.Exceptions;
using MyShop.Core.Interfaces;
using MyShop.Core.Interfaces.Services;
using MyShop.Core.Models;
using MyShop.Core.Options;
using MyShop.SharedProject.DTOs;

namespace MyShop.Core.Services;

public class ProductService: IProductService
{
    private readonly ProductServiceOptions _options;
    
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IOptions<ProductServiceOptions> options, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        
        return await _unitOfWork.ProductRepository
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
        
        var product = await _unitOfWork.ProductRepository.GetById(id);

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
            throw new ProductPoorPriceException();

        var newProduct = new Product()
        {
            Id = Guid.NewGuid(),
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            CategoryId = Guid.Parse(productDto.CategoryId),
            ImageUrl = productDto.ImageUrl
        };
        await _unitOfWork.ProductRepository.Add(newProduct);
        await _unitOfWork.SaveChangesAsync();
    }
}