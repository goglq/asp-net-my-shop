using System.ComponentModel.DataAnnotations;

namespace MyShop.SharedProject.DTOs;

public class CategoryDto
{
    [Required]
    public string? Name { get; set; }
}