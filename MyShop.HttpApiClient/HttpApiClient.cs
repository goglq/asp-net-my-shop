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

    public Task<HttpResponseMessage> CreateProduct(ProductDto product) => _httpClient.PostAsJsonAsync($"{_url}/product", product);

    public Task<HttpResponseMessage> RegisterAccount(RegistrationAccountDto registrationAccountDto) =>
        _httpClient.PostAsJsonAsync($"{_url}/account/register", registrationAccountDto);

    public async Task<HttpResponseMessage> LoginAccount(LoginAccountDto loginAccountDto)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_url}/account/login", loginAccountDto);
        return response;
    }
}