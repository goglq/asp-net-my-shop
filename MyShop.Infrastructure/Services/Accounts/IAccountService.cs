using MyShop.Models;
using MyShop.SharedProject;
using MyShop.SharedProject.DTOs;

namespace MyShop.Infrastructure.Services.Accounts;

public interface IAccountService
{
    public Task<string> Register(RegistrationAccountDto registrationAccountDto);

    public Task<string> Login(LoginAccountDto loginAccountDto);

    public Task<bool> IsEmailTaken(string email);

    public Task<IEnumerable<Account>> GetAccounts();
}