using System.ComponentModel.DataAnnotations;

namespace MyShop.Core.Models;

public class ConfirmationCode : IEntity
{
    [Key]
    public Guid Id { get; init; }
    
    public int Code { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public Guid AccountId { get; set; }
    
    public Account Account { get; set; }
}