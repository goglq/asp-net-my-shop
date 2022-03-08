using System.Net.Http.Json;
using MyShop.Models;
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

    public Task<IReadOnlyList<Product>?> GetAll() =>
        _httpClient.GetFromJsonAsync<IReadOnlyList<Product>>($"{_url}/api/product");

    public Task<Product?> Get(Guid id) =>
        _httpClient.GetFromJsonAsync<Product>($"{_url}/api/product/{id}");

    public Task Create(ProductDto product) => _httpClient.PostAsJsonAsync($"{_url}/api/product", product);
}