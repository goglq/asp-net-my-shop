namespace MyShop.Core.Exceptions;

public class IncorrectTwoFactorCodeException : Exception
{
    public IncorrectTwoFactorCodeException() : base("Incorrect code")
    {
        
    }
}