using MyShop.Core.Models;

namespace MyShop.Core.Interfaces.Services;

public interface IAccountService
{
    public Task<string> Register(string email, string name, string password);

    public Task<string> Login(string email, string password);

    public Task<bool> IsEmailTaken(string email);

    public Task<IEnumerable<Account>> GetAccounts();
}