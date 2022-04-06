using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Models;
using MyShop.Infrastructure.Databases;

namespace MyShop.Infrastructure.Repositories;

public class ConfirmationCodeRepository : EfRepository<ConfirmationCode>, IConfirmationCodeRepository
{
    public ConfirmationCodeRepository(AppDbContext dbContext) : base(dbContext) { }
}