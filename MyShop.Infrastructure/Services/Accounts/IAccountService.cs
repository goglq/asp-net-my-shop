using MyShop.SharedProject.DTOs;

namespace MyShop.Infrastructure.Services.Accounts;

public interface IAccountService
{
    public Task Register(AccountDto accountDto);

    public Task<bool> IsEmailTaken(string email);
}