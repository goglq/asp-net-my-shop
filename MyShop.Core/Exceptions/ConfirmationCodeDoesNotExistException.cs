namespace MyShop.Core.Exceptions;

public class ConfirmationCodeDoesNotExistException : MyShopException
{
    public ConfirmationCodeDoesNotExistException() : base("Confirmation code does not exist")
    {
    }
}