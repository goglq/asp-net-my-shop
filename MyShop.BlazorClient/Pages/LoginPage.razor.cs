using Microsoft.AspNetCore.Components;
using MyShop.SharedProject.DTOs;

namespace MyShop.BlazorClient.Pages;

public partial class LoginPage
{
    private readonly LoginAccountDto _model = new();

    private string? _message;

    private async Task ProcessValidSubmit()
    {
        try
        {
            var response = await _client.LoginAccount(_model);
            var token = response?.Model;
            if (token is not null)
            {
                await _localStorage.SetItemAsync("accessToken", token);
            }
        }
        catch (Exception ex)
        {
            _message = ex.Message;
        }
    }
}