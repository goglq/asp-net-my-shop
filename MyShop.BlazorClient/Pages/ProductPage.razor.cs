using Microsoft.AspNetCore.Components;
using MyShop.Core.Models;

namespace MyShop.BlazorClient.Pages;

public partial class ProductPage
{
    [Parameter]
    public Guid ProductId { get; set; }

    private Product? Product { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Product = await _client.GetProduct(ProductId);
    }
}