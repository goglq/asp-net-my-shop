using MyShop.Models;

namespace MyShop.HttpApiServer.Infrastructure.Repositories;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext) { }
}