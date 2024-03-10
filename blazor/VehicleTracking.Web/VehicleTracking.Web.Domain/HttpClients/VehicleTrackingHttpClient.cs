using Microsoft.Extensions.Caching.Memory;

namespace VehicleTracking.Web.Domain.HttpClients
{
    public class VehicleTrackingHttpClient : HttpClientBase
    {
        public VehicleTrackingHttpClient(HttpClient httpClient, IMemoryCache memoryCache)
        {
            HttpClient = httpClient;
            _memoryCache = memoryCache;
        }
    }
}
