using MyShop.SharedProject.DTOs;

namespace MyShop.Infrastructure.Services.Accounts;

public interface IAccountService
{
    public Task Register(RegistrationAccountDto registrationAccountDto);

    public Task<bool> Login(LoginAccountDto loginAccountDto);

    public Task<bool> IsEmailTaken(string email);
}