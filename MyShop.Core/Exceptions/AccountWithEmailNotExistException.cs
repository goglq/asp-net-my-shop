namespace MyShop.Core.Exceptions;

public class AccountWithEmailNotExistException : MyShopException
{
    public AccountWithEmailNotExistException() : base("Account with this Email does not exist.")
    {
    }
}