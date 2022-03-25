using System.ComponentModel.DataAnnotations;

namespace MyShop.Models;

public class Account : IEntity
{
    [Key]
    public Guid Id { get; init; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Role { get; set; }
}