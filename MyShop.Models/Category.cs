using System.ComponentModel.DataAnnotations;

namespace MyShop.Models;

public class Category
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public IEnumerable<Product> Products { get; set; }
}