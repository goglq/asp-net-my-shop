using MyShop.Core.Models;

namespace MyShop.Core.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(Account account);
}