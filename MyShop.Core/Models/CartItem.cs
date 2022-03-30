namespace MyShop.Core.Models;

public class CartItem : IEntity
{
    public Guid Id { get; init; }
    
    public int Count { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
}