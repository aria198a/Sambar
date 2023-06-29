using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Functions
{
    public static class MemoryCacheKey
    {
        public static string epa_station = "epa_station";
        public static string STA_AirQuality_v2 = "STA_AirQuality_v2";
        public static string FirstSaveTime = "FirstSaveTime";
    }

    public class MemoryCacheFunctions
    {
        private IMemoryCache _cache;

        public MemoryCacheFunctions(IMemoryCache cache)
        {
            _cache = cache;
        }
        
        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public void Get(string key, object obj)
        {
            _cache.Set(key, obj);
        }
    }
}