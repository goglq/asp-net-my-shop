using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Infrastructure.Services.Products;
using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        var products = await _productService.GetAll(skip, take, DateTime.Now.DayOfWeek, HttpContext.Request.Headers.UserAgent);
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> Get(Guid id)
    {
        var product = await _productService.Get(id, DateTime.Now.DayOfWeek, HttpContext.Request.Headers.UserAgent);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto productDto)
    {
        await _productService.Create(productDto);
        return Created(Uri.UriSchemeHttp, null);
    }
}