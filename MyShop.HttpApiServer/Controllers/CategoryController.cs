using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.HttpApiServer.Infrastructure.Repositories;
using MyShop.HttpApiServer.Services.Categories;
using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Controllers;

[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService, ICategoryRepository categoryRepository)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public Task<IEnumerable<Category>> GetAll() =>
        _categoryService.GetAll();

    [HttpGet("{id:guid}")]
    public Task<Category> Get(Guid id) =>
        _categoryService.Get(id);
    
    [HttpPost]
    public Task Create([FromBody] CategoryDTO categoryDto) => 
        _categoryService.Create(categoryDto);
}