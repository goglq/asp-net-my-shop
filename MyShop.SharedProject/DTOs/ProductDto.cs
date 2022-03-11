using System.ComponentModel.DataAnnotations;

namespace MyShop.SharedProject.DTOs;

public record ProductDto
{
    [Required]
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public string? CategoryId { get; set; }
    
    [Required]
    public string? ImageUrl { get; set; }
};