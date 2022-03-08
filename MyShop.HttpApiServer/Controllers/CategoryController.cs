using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.HttpApiServer.Infrastructure.Repositories;
using MyShop.HttpApiServer.Services.Categories;
using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryService categoryService, ICategoryRepository categoryRepository)
    {
        _categoryService = categoryService;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Category>> GetAll() => 
        await _categoryRepository
            .GetAll()
            .ToListAsync();

    [HttpGet("{id:guid}")]
    public Task<Category> Get(Guid id) =>
        _categoryRepository.GetById(id);
    
    [HttpPost]
    public Task Create([FromBody] CategoryDTO categoryDto) => 
        _categoryService.Create(categoryDto);
}