using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.Infrastructure.Services.Categories;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAll();

    Task<Category> Get(Guid id);
    
    Task Create(CategoryDto categoryDto);
}