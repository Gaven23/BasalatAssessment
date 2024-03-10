using Microsoft.Extensions.Caching.Memory;
using SmartHub.Web.Domain.Caching;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using VehicleTracking.Web.Domain.Caching;
using VehicleTracking.Web.Domain.Models;

namespace VehicleTracking.Web.Domain.HttpClients
{
    public class HttpClientBase : IHttpClientBase
    {
        protected IMemoryCache _memoryCache; // https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-6.0#use-imemorycache
        public HttpClient HttpClient { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<RequestResponse> DeleteAsync(string url, object body)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Delete, url);
                using var stringContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
                request.Content = stringContent;

                var response = await HttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return new(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return ErrorResponse(content, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return ExceptionResponse(ex, false);
            }
        }

        public async Task<RequestResponse<TResult>> GetAsJsonAsync<TResult>(string url, CancellationTokenSource? cancellationTokenSource = null, CacheTypes cacheType = CacheTypes.NoCaching)
        {
            try
            {
                string? cacheKey = null;
                int? cacheExpiration = null;
                if (cacheType != CacheTypes.NoCaching && CachingHelper.ShouldCacheRequest(cacheType, out cacheExpiration))
                {
                    cacheKey = url;
                    var cacheResult = TryGetCacheItem<TResult>(cacheKey);
                    if (cacheResult != null)
                        return new RequestResponse<TResult> { Result = cacheResult };
                }

                var response = (cancellationTokenSource is not null) switch
                {
                    true => await HttpClient.GetAsync(url, cancellationTokenSource.Token),
                    false => await HttpClient.GetAsync(url)
                };

                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return SuccessResponseCached<TResult>(content, response.StatusCode, cacheKey, cacheExpiration);
                }
                else
                {
                    return ErrorResponse<TResult>(content, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return ExceptionResponse<TResult>(ex, cancellationTokenSource?.IsCancellationRequested ?? false);
            }
        }

        public async Task<RequestResponse<TResult>> PostAsJsonAsync<TResult, TBody>(string url,
            TBody body, CancellationTokenSource? cancellationTokenSource = null,
            CacheTypes cacheType = CacheTypes.NoCaching, JsonSerializerOptions? serializerOptions = null)
        {
            try
            {
                string? cacheKey = null;
                int? cacheExpiration = null;
                if (cacheType != CacheTypes.NoCaching && CachingHelper.ShouldCacheRequest(cacheType, out cacheExpiration))
                {
                    cacheKey = $"{url}{JsonSerializer.Serialize(body)})";
                    var cacheResult = TryGetCacheItem<TResult>(cacheKey);
                    if (cacheResult != null)
                        return new RequestResponse<TResult> { Result = cacheResult };
                }

                var response = (cancellationTokenSource is not null) switch
                {
                    true => await HttpClient.PostAsJsonAsync(url, body, cancellationTokenSource.Token),
                    false => await HttpClient.PostAsJsonAsync(url, body)
                };

                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return SuccessResponseCached<TResult>(content, response.StatusCode, cacheKey, cacheExpiration, serializerOptions);
                }
                else
                {
                    return ErrorResponse<TResult>(content, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return ExceptionResponse<TResult>(ex, cancellationTokenSource?.IsCancellationRequested ?? false);
            }
        }

        public async Task<RequestResponse> PostAsJsonAsync<TBody>(string url, TBody body)
        {
            try
            {
                var response = await HttpClient.PostAsJsonAsync(url, body);

                if (response.IsSuccessStatusCode)
                {
                    return new(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return ErrorResponse(content, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return ExceptionResponse(ex, false);
            }
        }

        public Task<RequestResponse> PutAsJsonAsync(string url, object? body)
        {
            throw new NotImplementedException();
        }

        public static RequestResponse ErrorResponse(string content, HttpStatusCode statusCode)
        {
            return ErrorResponse<string>(content, statusCode);
        }

        public static RequestResponse ExceptionResponse(Exception ex, bool isCancellationRequested = false)
        {
            return ExceptionResponse<string>(ex, isCancellationRequested);
        }

        public static RequestResponse<TResult> ExceptionResponse<TResult>(Exception ex, bool isCancellationRequested = false)
        {
            if (ex is OperationCanceledException cancelEx)
            {
                return new RequestResponse<TResult>()
                {
                    IsSuccessful = false,
                    IsCancelled = isCancellationRequested,
                    Exception = cancelEx,
                };
            }
            else
            {
                return new RequestResponse<TResult>
                {
                    IsSuccessful = false,
                    Exception = ex
                };
            }
        }

        public static RequestResponse<TResult> ErrorResponse<TResult>(string content, HttpStatusCode statusCode)
        {
            var requestResponse = new RequestResponse<TResult>
            {
                IsSuccessful = false,
                StatusCode = statusCode,
            };

            if (IsProblemDetails(content))
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (problemDetails != null)
                {
                    requestResponse.Message = problemDetails.Title;
                    requestResponse.ErrorCode = problemDetails.ErrorCode;
                }
            }

            return requestResponse;
        }

        public static bool IsProblemDetails(string content)
        {
            return content != null && content.Contains("title") && content.Contains("errorCode") && content.Contains("traceId");
        }

        private T? TryGetCacheItem<T>(string cacheKey)
        {
            var fullCacheKey = GetFullCacheKey(cacheKey);

            if (_memoryCache.TryGetValue(fullCacheKey, out T? value))
            {
                return value;
            }
            else
            {
                return default;
            }
        }

        private string GetFullCacheKey(string cacheKey) => HttpClient.BaseAddress + cacheKey;

        private void SetCacheItem<T>(string cacheKey, int cacheInternal, T value)
        {
            var fullCacheKey = GetFullCacheKey(cacheKey);
            _memoryCache.Set(fullCacheKey, value, TimeSpan.FromMinutes(cacheInternal));
        }
        private RequestResponse<TResult> SuccessResponseCached<TResult>(string content, HttpStatusCode statusCode,
    string? cacheKey = null, int? cacheExpiration = null, JsonSerializerOptions? serializerOptions = null)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                var options = serializerOptions ?? new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<TResult>(content, options);

                if (cacheKey != null && cacheExpiration != null && result != null)
                    SetCacheItem(cacheKey, cacheExpiration.Value, result);

                return new RequestResponse<TResult>(statusCode)
                {
                    Result = result
                };
            }
            else
            {
                return new RequestResponse<TResult>(statusCode)
                {
                    Result = default
                };
            }
        }
    }
}
