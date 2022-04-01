using MyShop.Core.Models;

namespace MyShop.Core.Interfaces.Repositories;

public interface ICartItemRepository : IRepository<CartItem>
{
    public Task AddProduct(Cart cart, Guid productId);
}