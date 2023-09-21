using StackExchange.Redis;

namespace koszalka_api.Caching
{
    public interface ICacheService
    {
        string GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        string AppendData(RedisKey key, RedisValue value);
        object RemoveData(string key);
        string GetAppendedData(RedisKey key);
    }
}
