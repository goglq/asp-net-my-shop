using Microsoft.AspNetCore.Components;
using MyShop.SharedProject.DTOs;

namespace MyShop.BlazorClient.Pages;

public partial class RegistrationPage
{
    [Inject] private NavigationManager _navigationManager { get; set; }
    
    private readonly RegistrationAccountDto _model = new();

    private string? _responseString = "";

    private async Task ProcessValidSubmit()
    {
        try
        {
            var response = await _client.RegisterAccount(_model);
            _navigationManager.NavigateTo("/login");
        }
        catch (Exception ex)
        {
            _responseString = ex.Message;
        }
    }
}