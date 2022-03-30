using MyShop.Core.Models;

namespace MyShop.BlazorClient.Pages;

public partial class ProductsPage
{
    private IReadOnlyList<Product>? _products; 

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        _products = await _client.GetProducts(0, 30);
        
        StateHasChanged();
    }
}