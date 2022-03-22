using System.Net.Http.Json;
using MyShop.Models;
using MyShop.SharedProject;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiClient;

public class HttpApiClient : IHttpApiClient
{
    private readonly HttpClient _httpClient;

    private readonly string _url;

    public HttpApiClient(string url, HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient();
        _url = url;
    }

    public Task<IReadOnlyList<Product>?> GetProducts(int skip, int take) =>
        _httpClient.GetFromJsonAsync<IReadOnlyList<Product>>($"{_url}/product?skip={skip}&take={take}");

    public Task<Product?> GetProduct(Guid id) =>
        _httpClient.GetFromJsonAsync<Product>($"{_url}/product/{id}");

    public Task<ResponseMessage<string>?> CreateProduct(ProductDto product) => 
        _httpClient.PostAsJsonAsync<ProductDto, ResponseMessage<string>>($"{_url}/product", product);

    public Task<ResponseMessage<string>?> RegisterAccount(RegistrationAccountDto registrationAccountDto) =>
        _httpClient.PostAsJsonAsync<RegistrationAccountDto, ResponseMessage<string>>($"{_url}/account/register", registrationAccountDto);

    public Task<ResponseMessage<string>?> LoginAccount(LoginAccountDto loginAccountDto) => 
        _httpClient.PostAsJsonAsync<LoginAccountDto, ResponseMessage<string>>($"{_url}/account/login", loginAccountDto);
}