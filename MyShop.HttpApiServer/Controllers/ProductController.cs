using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.HttpApiServer.Infrastructure.Repositories;
using MyShop.HttpApiServer.Services.Products;
using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Controllers;

[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public Task<IEnumerable<Product>> Get([FromQuery] int skip = 0, [FromQuery] int take = 10) =>
        _productService.GetAll(skip, take);

    [HttpGet("{id:guid}")]
    public Task<Product> Get(Guid id) =>
        _productService.Get(id);
    
    [HttpPost]
    public Task Create([FromBody] ProductDto productDto) => 
        _productService.Create(productDto);
}