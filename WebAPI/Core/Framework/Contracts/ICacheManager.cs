using System;

namespace Core.Framework.Contracts
{
    public interface ICacheManager
    {
        bool IsSet(string key);

        void Remove(string key);

        void RemoveByPattern(string pattern);

        void Clear();

        T GetCachedObject<T>(string cacheKey, int timeSpan, Func<T> acquire);
    }
}
