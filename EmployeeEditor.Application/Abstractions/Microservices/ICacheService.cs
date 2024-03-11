namespace EmployeeEditor.Application.Abstractions.Microservices
{
    public interface ICacheService
    {
        Task<T> GetCacheData<T>(string key);
        Task<object> RemoveData(string key);
        Task<bool> SetCacheData<T>(string key, T value, DateTimeOffset expirationTime);
    }
}
