using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace BaseApi.BLL
{
    public class MemCache
    {
        private static MemoryCache cache = MemoryCache.Default;
        public static object Get(string key)
        {
            return cache.Get(key);
        }
        public static void Add(string key, object value, long expire = 0)
        {

            cache.Add(new CacheItem(key, value), new CacheItemPolicy() { AbsoluteExpiration = CreateOffset(expire) });
        }

        public static void Set(string key, object value, long expire = 0)
        {
            cache.Set(key, value, CreateOffset(expire));
        }
        public static object Remove(string key)
        {
            return cache.Remove(key);
        }

        public static void SetExpired(string key, long expire)
        {
            cache.Set(key, cache.Get(key), CreateOffset(expire));
        }
        static private DateTimeOffset CreateOffset(long expire)
        {
            DateTimeOffset offset;
            if (expire <= 0)
            {
                offset = ObjectCache.InfiniteAbsoluteExpiration;
            }
            else
            {
                offset = DateTimeOffset.Now.AddSeconds(expire);
            }
            return offset;
        }
        public static List<string> Keys()
        {
            List<string> list = new List<string>();
            IEnumerable<KeyValuePair<string, object>> items = cache.AsEnumerable();
            foreach (KeyValuePair<string, object> item in items)
            {
                list.Add(item.Key);
            }
            return list;
        }
    }
}