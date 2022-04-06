using MyShop.Core.Models;

namespace MyShop.Core.Interfaces.Repositories;

public interface IConfirmationCodeRepository : IRepository<ConfirmationCode>
{
    Task<Account> GetUserByCodeId(Guid id);
}