using MyShop.Core.Interfaces;
using MyShop.Core.Interfaces.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;

    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<IEnumerable<CartItem>> GetCartByUserId(Guid id) => 
        _unitOfWork.CartRepository.GetCartItemsByUserId(id);

    public async Task AddProduct(Guid accountId, Guid productId)
    {
        var cart = await _unitOfWork.CartRepository.GetCartByUserId(accountId);
        await _unitOfWork.CartItemRepository.AddProduct(cart, productId);
        await _unitOfWork.SaveChangesAsync();
    }
}