using System.ComponentModel.DataAnnotations;

namespace MyShop.SharedProject.DTOs;

public record LoginAccountDto
{
    [Required, EmailAddress]
    public string? Email { get; set; }
    
    [Required, MinLength(6), MaxLength(16)]
    public string? Password { get; set; }
}