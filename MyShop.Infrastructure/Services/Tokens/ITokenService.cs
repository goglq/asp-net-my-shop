using MyShop.Models;

namespace MyShop.Infrastructure.Services.Tokens;

public interface ITokenService
{
    string GenerateToken(Account account);
}