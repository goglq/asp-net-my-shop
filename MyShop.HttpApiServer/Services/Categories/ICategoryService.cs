using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Services.Categories;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAll();

    Task<Category> Get(Guid id);
    
    Task Create(CategoryDTO categoryDto);
}