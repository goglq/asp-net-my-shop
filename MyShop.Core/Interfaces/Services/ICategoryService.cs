using MyShop.Core.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.Core.Interfaces.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAll();

    Task<Category> Get(Guid id);
    
    Task Create(CategoryDto categoryDto);
}