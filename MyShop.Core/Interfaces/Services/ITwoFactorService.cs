using MyShop.Core.Models;

namespace MyShop.Core.Interfaces.Services;

public interface ITwoFactorService
{
    int GenerateCode(int length);

    Task<Guid> CreateCode(Guid accountId, int code);

    Task<bool> IsCorrectCode(Guid codeId, int code);
}