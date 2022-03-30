using Microsoft.EntityFrameworkCore;
using MyShop.Core.Interfaces;
using MyShop.Core.Interfaces.Services;
using MyShop.Core.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.Core.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>> GetAll() => await _unitOfWork.CategoryRepository.GetAll().ToListAsync();

    public Task<Category> Get(Guid id) => _unitOfWork.CategoryRepository.GetById(id);

    public async Task Create(CategoryDto categoryDto)
    {
        var newCategory = new Category()
        {
            Id = Guid.NewGuid(),
            Name = categoryDto.Name
        };
        await _unitOfWork.CategoryRepository.Add(newCategory);
        await _unitOfWork.SaveChangesAsync();
    }
}