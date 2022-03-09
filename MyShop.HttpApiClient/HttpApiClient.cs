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

    public Task<IReadOnlyList<Product>?> GetAll(int skip, int take) =>
        _httpClient.GetFromJsonAsync<IReadOnlyList<Product>>($"{_url}/product?skip={skip}&take={take}");

    public Task<Product?> Get(Guid id) =>
        _httpClient.GetFromJsonAsync<Product>($"{_url}/product/{id}");

    public Task Create(ProductDto product) => _httpClient.PostAsJsonAsync($"{_url}/product", product);
}