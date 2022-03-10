using System.ComponentModel.DataAnnotations;

namespace MyShop.Models;

public class Product : IEntity
{
    [Key]
    public Guid Id { get; init; }
    
    [Required]
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public int Count { get; set; }
    
    public string ImageUrl { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public Guid CategoryId { get; set; }
    
    public Category Category { get; set; }
}