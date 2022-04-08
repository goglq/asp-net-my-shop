using MyShop.Core.Models;

namespace MyShop.Core.Interfaces.Services;

public interface IAccountService
{
    Task<string> Register(string email, string name, string password);

    Task<string> Login(string email, string password);

    Task<Guid> LoginTwoFactor(string email, string password);

    Task<bool> IsEmailTaken(string email);

    Task<IEnumerable<Account>> GetAccounts();
    
    Task<bool> CheckIsBannedById(Guid userId);
}