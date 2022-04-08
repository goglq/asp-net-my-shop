namespace MyShop.Core.Exceptions;

public class EmailIsTakenException : MyShopException
{
    public EmailIsTakenException() : base("Email is taken.")
    {
        
    }
}