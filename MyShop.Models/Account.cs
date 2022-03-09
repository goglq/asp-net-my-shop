namespace MyShop.Models;

public class Account : IEntity
{
    public Guid Id { get; init; }
    
    public string Email { get; set; }
}