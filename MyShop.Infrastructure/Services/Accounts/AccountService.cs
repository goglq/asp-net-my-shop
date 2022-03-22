using MyShop.Infrastructure.Repositories;
using MyShop.Models;
using MyShop.SharedProject.DTOs;
using Microsoft.AspNetCore.Identity;

namespace MyShop.Infrastructure.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    private readonly IPasswordHasher<Account> _passwordHasher;
    
    public AccountService(IAccountRepository accountRepository, IPasswordHasher<Account> passwordHasher)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task Register(RegistrationAccountDto registrationAccountDto)
    {
        if (registrationAccountDto.Email is null)
            throw new ArgumentNullException(nameof(registrationAccountDto.Email),"Email is null");
        if (await IsEmailTaken(registrationAccountDto.Email))
            throw new Exception("Email is taken");
        
        var account = new Account()
        {
            Id = Guid.NewGuid(),
            Name = registrationAccountDto.Name,
            Email = registrationAccountDto.Email,
            Password = registrationAccountDto.Password
        };

        var hashedPassword = _passwordHasher.HashPassword(account, registrationAccountDto.Password);
        account.Password = hashedPassword;
        await _accountRepository.Add(account);
        await _accountRepository.Save();
    }

    public async Task<bool> Login(LoginAccountDto loginAccountDto)
    {
        var account = await _accountRepository.FindByEmail(loginAccountDto.Email!);
        if (account is null)
            throw new NullReferenceException("Account with this email does not exist");
        var verifyResult = _passwordHasher.VerifyHashedPassword(account, account.Password, loginAccountDto.Password);
        if (verifyResult == PasswordVerificationResult.Failed)
            throw new ArgumentException("Incorrect password");
        return true;
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        var account = await _accountRepository.FindByEmail(email);
        return account is not null;
    }
}