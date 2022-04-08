namespace MyShop.Core.Exceptions;

public abstract class MyShopException : Exception
{
    protected MyShopException(string message) : base(message) { }
}