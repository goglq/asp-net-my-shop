namespace MyShop.Core.Options;

public class CorsSettings
{
    public IReadOnlyList<string> Origins { get; set; }
    public IReadOnlyList<string> Methods { get; set; }
}