using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.HttpApiServer.Infrastructure.Repositories;
using MyShop.HttpApiServer.Services.Products;
using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductRepository _productRepository;

    public ProductController(IProductService productService, IProductRepository productRepository)
    {
        _productService = productService;
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> Get([FromQuery] int skip = 0, [FromQuery] int take = 10) => 
        await _productRepository
            .GetAll()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

    [HttpGet("{id:guid}")]
    public Task<Product> Get(Guid id) =>
        _productRepository.GetById(id);
    
    [HttpPost]
    public Task Create([FromBody] ProductDto productDto) => 
        _productService.Create(productDto);
}