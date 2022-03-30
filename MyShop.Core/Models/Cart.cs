namespace MyShop.Core.Models;

public class Cart : IEntity
{
    public Guid Id { get; init; }
    
    public Guid AccountId { get; set; }
    
    public Account Account { get; set; }
    
    public IList<CartItem> CartItems { get; set; }
}