using System.Text;

namespace MyShop.Core.Options;

public class JwtConfig
{
    public string SigningKey { get; init; }
    
    public TimeSpan Lifetime { get; init; }
    
    public string Audience { get; init; }
    
    public string Issuer { get; init; }
    
    public byte[] SigningKeyBytes => Encoding.UTF8.GetBytes(SigningKey);
}