using MyShop.Core.Models;

namespace MyShop.Core.Interfaces.Repositories;

public interface ICartRepository : IRepository<Cart>
{
    Task<IEnumerable<CartItem>> GetCartItemsByUserId(Guid id);

    Task<Cart> GetCartByUserId(Guid id);

    Task AddProduct(Guid accountId, Guid productId);
}