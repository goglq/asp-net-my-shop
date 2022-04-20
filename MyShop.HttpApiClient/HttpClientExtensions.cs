using System.Net.Http.Json;
using System.Net.Mime;
using MyShop.SharedProject;

namespace MyShop.HttpApiClient
{

    class ValidationProblemDetails
    {
        public string? Title { get; set; }
        
    }

    public static class HttpClientExtensions
    {
        public static async Task<TResponse?> PostAsJsonAsync<TRequest, TResponse>(this HttpClient client,
            string? requestUrl, TRequest request)
        {
            using var response = await client.PostAsJsonAsync(requestUrl, request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TResponse>();
            }

            if (response.Content.Headers.ContentType?.MediaType == MediaTypeNames.Application.Json)
            {
                var details = await response.Content.ReadFromJsonAsync<ResponseMessage<ValidationProblemDetails>>();
                throw new Exception(details?.Message);
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }
    }
}