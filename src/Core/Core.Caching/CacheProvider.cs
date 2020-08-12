using Core.Enumarations;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Core.Caching
{
    public class CacheProvider
    {
        private readonly IDistributedCache _distributedCache;
        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Include,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new DefaultContractResolver(),
        };
        public CacheProvider(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task RemoveAsync(string cacheKey, string cacheName)
        {
            var cacheResult = cacheKey + cacheName;
            await _distributedCache.RemoveAsync(cacheResult);
        }
        public async Task<T> GetOrAddAsync<T>(string cacheKey, string cacheName, TimeSpan timeSpan, ExpirationMode expirationMode, Func<Task<T>> valueProvider) where T : class
        {
            var cacheResult = cacheKey + cacheName;
            var cacheValue = await _distributedCache.GetAsync(cacheResult);
            if (cacheValue != null)
            {
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(cacheValue), jsonSerializerSettings);
            }
            var value = await valueProvider();

            if (value != null)
            {
                var serialized = JsonConvert.SerializeObject(value, jsonSerializerSettings);
                var bytes = Encoding.UTF8.GetBytes(serialized);
                var cacheOptions = new DistributedCacheEntryOptions();

                if (expirationMode == ExpirationMode.Absolute)
                {
                    cacheOptions.AbsoluteExpirationRelativeToNow = timeSpan;
                }
                else
                {
                    cacheOptions.SlidingExpiration = timeSpan;
                }
                await _distributedCache.SetAsync(cacheResult, bytes, cacheOptions);
                return value;
            }
            return null;
        }
    }
}
