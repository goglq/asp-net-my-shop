using System.Text;

namespace MyShop.Infrastructure.Options;

public record JwtConfig(string SigningKey, TimeSpan Lifetime, string Audience, string Issuer)
{
    public byte[] SigningKeyBytes => Encoding.UTF8.GetBytes(SigningKey);
}