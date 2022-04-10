using System.Text;
using MyShop.Core.Exceptions;
using MyShop.Core.Interfaces;
using MyShop.Core.Interfaces.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IEmailSender _emailSender;
    
    public CartService(IUnitOfWork unitOfWork, IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
    }
    
    public Task<IEnumerable<CartItem>> GetCartByUserId(Guid id) => 
        _unitOfWork.CartRepository.GetCartItemsByUserId(id);

    public async Task AddProduct(Guid accountId, Guid productId)
    {
        var cart = await _unitOfWork.CartRepository.GetCartByUserId(accountId);
        await _unitOfWork.CartItemRepository.AddProduct(cart, productId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Order(Guid accountId)
    {
        var account = await _unitOfWork.AccountRepository.FindById(accountId);
        if (account is null)
            throw new AccountNotExistException();
        var cartItems = (await _unitOfWork.CartRepository.GetCartItemsByUserId(accountId)).ToList();
        if (!cartItems.Any())
            throw new EmptyCartException();
        var stringBuilder = new StringBuilder();
        foreach (var cartItem in cartItems)
        {
            stringBuilder.Append($"{cartItem.Product.Name} {cartItem.Product.Price} {cartItem.Count}\n");
        }
        var message = _emailSender.CreateMessage("purps.auth.test@gmail.com", account.Email, "Order", stringBuilder.ToString());
        _emailSender.SendEmail(message);
        await ClearCart(cartItems);
    }

    public async Task ClearCart(Guid accountId)
    {
        var cartItems = await _unitOfWork.CartRepository.GetCartItemsByUserId(accountId);
        await ClearCart(cartItems);
    }

    public async Task ClearCart(IEnumerable<CartItem> cartItems)
    {
        foreach (var cartItem in cartItems)
            _unitOfWork.CartItemRepository.Delete(cartItem);
        await _unitOfWork.SaveChangesAsync();
    }
}