namespace MyShop.Core.Exceptions;

public class AccountNotExistException : MyShopException
{
    public AccountNotExistException() : base("Account does not exist")
    {
    }
}