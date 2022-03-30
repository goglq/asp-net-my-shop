using MyShop.Core.Models;

namespace MyShop.Core.Interfaces.Services;

public interface ICartService
{
    Task<IEnumerable<CartItem>> GetCartByUserId(Guid id);
}