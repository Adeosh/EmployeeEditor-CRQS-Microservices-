using EmployeeEditor.Application.Abstractions.Microservices;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace EmployeeEditor.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _dataBase;

        public CacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _dataBase = connectionMultiplexer.GetDatabase();
        }

        public async Task<T> GetCacheData<T>(string key)
        {
            var value = await _dataBase.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        public async Task<object> RemoveData(string key)
        {
            bool isKeyExist = await _dataBase.KeyExistsAsync(key);
            if (isKeyExist is true)
            {
                return _dataBase.KeyDeleteAsync(key);
            }

            return false;
        }

        public async Task<bool> SetCacheData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = await _dataBase.StringSetAsync(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }
    }
}
