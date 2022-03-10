using Microsoft.EntityFrameworkCore;
using MyShop.Infrastructure.Repositories;
using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.Infrastructure.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAll() => await _categoryRepository.GetAll().ToListAsync();

    public Task<Category> Get(Guid id) => _categoryRepository.GetById(id);

    public async Task Create(CategoryDTO categoryDto)
    {
        var newCategory = new Category()
        {
            Id = Guid.NewGuid(),
            Name = categoryDto.Name
        };
        await _categoryRepository.Add(newCategory);
        await _categoryRepository.Save();
    }
}