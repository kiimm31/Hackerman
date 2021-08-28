using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace ActionApi.Factory
{
    public static class CacheOptionFactory
    {
        public static DistributedCacheEntryOptions GenerateDistributedCacheEntryOptions()
        {
            return new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
        }
    }
}
