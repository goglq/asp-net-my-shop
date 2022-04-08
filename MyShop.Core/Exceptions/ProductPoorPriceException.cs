namespace MyShop.Core.Exceptions;

public class ProductPoorPriceException : MyShopException
{
    public ProductPoorPriceException() : base("Product price is less than 5")
    {
    }
}