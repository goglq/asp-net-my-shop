using MyShop.HttpApiServer.Infrastructure;
using MyShop.Models;

namespace MyShop.Infrastructure.Repositories;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext) { }
}