using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyShop.Core.Interfaces;
using MyShop.Core.Interfaces.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;

public class AccountService : IAccountService
{
    private readonly ITokenService _tokenService;

    private readonly IPasswordHasher<Account> _passwordHasher;
    
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(ITokenService tokenService, IUnitOfWork unitOfWork, IPasswordHasher<Account> passwordHasher)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Register(string email, string name, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is empty", nameof(email));
        if (await IsEmailTaken(email))
            throw new Exception("Email is taken");
        
        var account = new Account()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            Password = password,
            Role = "user"
        };

        var cart = new Cart()
        {
            Id = Guid.NewGuid(),
            AccountId = account.Id
        };
        
        var hashedPassword = _passwordHasher.HashPassword(account, password);
        account.Password = hashedPassword;
        await _unitOfWork.AccountRepository.Add(account);
        await _unitOfWork.CartRepository.Add(cart);
        await _unitOfWork.SaveChangesAsync();
        var token = _tokenService.GenerateToken(account);
        return token;
    }

    public async Task<string> Login(string email, string password)
    {
        var account = await _unitOfWork.AccountRepository.FindByEmail(email);
        if (account is null)
            throw new NullReferenceException("Account with this email does not exist");
        var verifyResult = _passwordHasher.VerifyHashedPassword(account, account.Password, password);
        if (verifyResult == PasswordVerificationResult.Failed)
            throw new ArgumentException("Incorrect password");
        var token = _tokenService.GenerateToken(account);
        return token;
    }

    public async Task<Guid> LoginTwoFactor(string email, string password)
    {
        var account = await _unitOfWork.AccountRepository.FindByEmail(email);
        if (account is null)
            throw new NullReferenceException("Account with this email does not exist");
        var verifyResult = _passwordHasher.VerifyHashedPassword(account, account.Password, password);
        if (verifyResult == PasswordVerificationResult.Failed)
            throw new ArgumentException("Incorrect password");
        return account.Id;
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        var account = await _unitOfWork.AccountRepository.FindByEmail(email);
        return account is not null;
    }

    public async Task<IEnumerable<Account>> GetAccounts() => 
        await _unitOfWork.AccountRepository.GetAll().ToListAsync();
}