using System.ComponentModel.DataAnnotations;

namespace MyShop.SharedProject.DTOs;

public record RegistrationAccountDto
{
    [Required]
    public string? Name { get; set; }
    
    [Required, EmailAddress]
    public string? Email { get; set; }
    
    [Required, MinLength(6), MaxLength(16)]
    public string? Password { get; set; }
    
    [Required, MinLength(6), MaxLength(16)]
    public string? RepeatPassword { get; set; }
}