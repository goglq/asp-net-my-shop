using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.HttpApiServer.Infrastructure.Repositories;
using MyShop.HttpApiServer.Services.Categories;
using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService, ICategoryRepository categoryRepository)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        var categories = await _categoryService.GetAll();

        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Category>> Get(Guid id)
    {
        var category = await _categoryService.Get(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDTO categoryDto)
    {
        await _categoryService.Create(categoryDto);

        return Created(Uri.UriSchemeHttp, null);
    }
}