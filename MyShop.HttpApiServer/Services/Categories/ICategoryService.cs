using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Services.Categories;

public interface ICategoryService
{
    Task Create(CategoryDTO categoryDto);
}