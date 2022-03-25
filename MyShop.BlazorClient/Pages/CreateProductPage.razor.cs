using Microsoft.AspNetCore.Components;
using MyShop.SharedProject.DTOs;

namespace MyShop.BlazorClient.Pages;

public partial class CreateProductPage
{
    [Inject] private NavigationManager _navigationManager { get; set; }
    
    private readonly ProductDto _productDto = new();

    private async Task ProcessValidForm()
    {
        try
        {
            var response = await _client.CreateProduct(_productDto);
            _navigationManager.NavigateTo("/products");
        }
        catch(Exception ex)
        {
            // ignore
        }
    }
}