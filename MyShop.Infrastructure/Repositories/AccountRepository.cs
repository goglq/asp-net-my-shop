using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Infrastructure.Repositories;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext) { }
    
    public Task<Account> GetByEmail(string email) => 
        Entities.FirstAsync(account => account.Email == email);
}