using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MyShop.HttpApiClient;

namespace MyShop.BlazorClient.Pages;

public abstract class AppComponentBase : ComponentBase
{
    [Inject] protected IHttpApiClient _client { get; set; }
    
    [Inject] protected ILocalStorageService _localStorage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var accessToken = await _localStorage.GetItemAsync<string>("accessToken");
        _client.SetToken(accessToken);
    }
}