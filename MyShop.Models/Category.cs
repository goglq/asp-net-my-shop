using System.ComponentModel.DataAnnotations;

namespace MyShop.Models;

public class Category : IEntity
{
    [Key]
    public Guid Id { get; init; }
    
    [Required]
    public string Name { get; set; }
    
    public IEnumerable<Product> Products { get; set; }
}