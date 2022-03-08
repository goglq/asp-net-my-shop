using MyShop.HttpApiServer.Infrastructure.Repositories;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Services.Products;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task Create(ProductDto productDto)
    {
        if (productDto.Price < 5)
            throw new ArgumentException("Product Price Cannot Be Less Than 5");
        
        var newProduct = new Models.Product()
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