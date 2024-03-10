using SmartHub.Web.Domain.Caching;
using System.Text.Json;
using VehicleTracking.Web.Domain.Models;

namespace VehicleTracking.Web.Domain.HttpClients
{
    internal interface IHttpClientBase
    {
        HttpClient HttpClient { get; set; }
        Task<RequestResponse<TResult>> PostAsJsonAsync<TResult, TBody>(string url, TBody body, CancellationTokenSource? cancellationTokenSource = null, CacheTypes cacheType = CacheTypes.NoCaching, JsonSerializerOptions? serializerOptions = null);
        Task<RequestResponse> PostAsJsonAsync<TBody>(string url, TBody body);
        Task<RequestResponse> PutAsJsonAsync(string url, object? body);
        Task<RequestResponse<TResult>> GetAsJsonAsync<TResult>(string url, CancellationTokenSource? cancellationTokenSource = null, CacheTypes cacheType = CacheTypes.NoCaching);
        Task<RequestResponse> DeleteAsync(string url, object body);
    }
}
