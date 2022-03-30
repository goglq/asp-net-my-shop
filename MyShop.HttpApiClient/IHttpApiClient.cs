using MyShop.Core.Models;
using MyShop.Core;
using MyShop.SharedProject;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiClient;

public interface IHttpApiClient
{
    void SetToken(string token);

    Task<IReadOnlyList<Account>?> GetAccounts();

    Task<IReadOnlyList<Product>?> GetProducts(int skip = 0, int take = 10);

    Task<Product?> GetProduct(Guid id);

    Task<ResponseMessage<string>?> CreateProduct(ProductDto product);

    Task<ResponseMessage<string>?> RegisterAccount(RegistrationAccountDto registrationAccountDto);

    Task<ResponseMessage<string>?> LoginAccount(LoginAccountDto loginAccountDto);
}