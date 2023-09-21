using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using StackExchange.Redis;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace koszalka_api.Caching
{
    public class CacheService : ICacheService
    {
        private IDatabase _db;
        public CacheService()
        {
            ConfigureRedis();
        }
        private void ConfigureRedis()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }
        public string GetData<T>(string key)
        {
            RedisValue value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            return default;
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }
        public object RemoveData(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }

        public string AppendData( RedisKey key, RedisValue value)
        {
            RedisValue appendRedisValue = _db.StreamAdd(key, "key", value);
            return appendRedisValue.ToString();
        }

        public string GetAppendedData(RedisKey key)
        {
            return _db.StreamRead(key, "0-0").ToJson();
        }

     
    }
}
