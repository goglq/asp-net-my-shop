using Microsoft.AspNetCore.Identity;
using MyShop.Infrastructure.Repositories;
using MyShop.Models;
using MyShop.SharedProject.DTOs;

namespace MyShop.Infrastructure.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task Register(AccountDto accountDto)
    {
        var account = new Account()
        {
            Id = Guid.NewGuid(),
            Name = accountDto.Name,
            Email = accountDto.Email,
            Password = accountDto.Password
        };

        await _accountRepository.Add(account);
        await _accountRepository.Save();
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        var account = await _accountRepository.FindByEmail(email);
        return account is not null;
    }
}