namespace MyShop.Core.Exceptions;

public class InvalidPasswordException : MyShopException
{
    public InvalidPasswordException() : base("Invalid password.")
    {
    }
}