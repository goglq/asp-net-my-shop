using Microsoft.EntityFrameworkCore;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Models;
using MyShop.Infrastructure.Databases;

namespace MyShop.Infrastructure.Repositories;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext) { }
    
    public Task<Account> GetByEmail(string email) => 
        Entities.FirstAsync(account => account.Email == email);

    public Task<Account?> FindByEmail(string email) =>
        Entities.FirstOrDefaultAsync(account => account.Email == email);
}