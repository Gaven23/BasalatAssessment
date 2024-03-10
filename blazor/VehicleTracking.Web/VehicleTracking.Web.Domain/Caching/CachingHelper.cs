using SmartHub.Web.Domain.Caching;

namespace VehicleTracking.Web.Domain.Caching
{
    public static class CachingHelper
    {
        private static readonly Dictionary<CacheTypes, int> _cacheExpirations = new()
            {
       
            };

        /// <summary>
        /// Determines whether the request should be cached, and returns the cache expiration in minutes.
        /// </summary>
        public static bool ShouldCacheRequest(CacheTypes cacheType, out int? cacheExpiration)
        {
            if (_cacheExpirations.TryGetValue(cacheType, out var expiration))
            {
                cacheExpiration = expiration;
                return true;
            }

            cacheExpiration = null;
            return false;
        }
    }
}
