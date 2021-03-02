using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebjetMovieFactory.Services.Interfaces;

namespace WebjetMovieFactory.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;

        public CacheService(IMemoryCache cache, IConfiguration config)
        {
            _cache = cache;
            _config = config;
        }

        public T GetFromCache<T>(string key) where T : class
        {
            var cachedResponse = _cache.Get(key);
            return cachedResponse as T;
        }

        public void SetCache<T>(string key, T value) where T : class
        {
            _cache.Set(key, value, DateTimeOffset.Now.AddMinutes(_config.GetValue<double>("CacheExpirationInMins")));
        }

        public void ClearCache(string key)
        {
            _cache.Remove(key);
        }
    }
}
