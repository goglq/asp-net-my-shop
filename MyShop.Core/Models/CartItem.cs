using System.ComponentModel.DataAnnotations;

namespace MyShop.Core.Models;

public class CartItem : IEntity
{
    [Key]
    public Guid Id { get; init; }
    
    [Required]
    public int Count { get; set; }
    
    [Required]
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; }
    
    [Required]
    public Guid CartId { get; set; }
    
    public Cart Cart { get; set; }
}