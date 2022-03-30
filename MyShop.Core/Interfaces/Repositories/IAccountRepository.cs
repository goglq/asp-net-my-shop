using MyShop.Core.Models;

#nullable enable
namespace MyShop.Core.Interfaces.Repositories;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account> GetByEmail(string email);
    
    Task<Account?> FindByEmail(string email);
}