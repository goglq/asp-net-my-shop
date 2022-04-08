using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Interfaces.Services;
using MyShop.Core.Models;
using MyShop.HttpApiServer.Filters;
using MyShop.SharedProject;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Controllers;

[Route("[controller]")]
[ApiController]
[ServiceFilter(typeof(ProfileActionFilter))]
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
    public async Task<ActionResult<ResponseMessage<ProblemDetails>>> Create(ProductDto productDto)
    {
        try
        {
            await _productService.Create(productDto);
            return Created(Uri.UriSchemeHttp, new ResponseMessage<ProblemDetails>("Product Created!", true));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseMessage<ProblemDetails>(ex.Message, false, new ProblemDetails()
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
            }));
        }
    }
}