namespace MyShop.Core.Exceptions;

public class EmptyCartException : MyShopException
{
    public EmptyCartException() : base("Cart is empty.")
    {
    }
}