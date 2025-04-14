using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GalaxyMatchGUI.Services
{
    public class HttpService
    {
        private static readonly Lazy<HttpService> _instance = new(() => new HttpService());
        private readonly HttpClient _httpClient;

        public static HttpService Instance => _instance.Value;

        private HttpService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(App.Settings.BackendUrl)
            };
        }

        public async Task<string> GetStringAsync(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }

        public async Task<T?> GetJsonAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var rawContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {response.StatusCode}, Content: {rawContent}");
            }

            return JsonSerializer.Deserialize<T>(rawContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<T?> PostJsonAsync<T>(string url, object data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            var rawContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"POST Error: {response.StatusCode}, Content: {rawContent}");
            }

            return JsonSerializer.Deserialize<T>(rawContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"DELETE Error: {response.StatusCode}, Content: {content}");
            }
        }
    }
}
