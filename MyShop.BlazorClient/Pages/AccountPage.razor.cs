using Microsoft.AspNetCore.Components;
using MyShop.Models;

namespace MyShop.BlazorClient.Pages;

public partial class AccountsPage
{
    private IReadOnlyList<Account>? _accounts;

    private bool _isNotAllowed;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await base.OnInitializedAsync();
            _accounts = await _client.GetAccounts();
        }
        catch (Exception _)
        {
            _isNotAllowed = true;
        }
    }
}