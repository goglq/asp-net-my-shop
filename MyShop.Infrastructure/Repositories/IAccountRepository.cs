using MyShop.Models;

namespace MyShop.Infrastructure.Repositories;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account> GetByEmail(string email);
    
    Task<Account?> FindByEmail(string email);
}