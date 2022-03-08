using MyShop.HttpApiServer.Infrastructure.Repositories;
using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
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