namespace FashionShop_API.Services.Caching;

public interface IServiceCacheRedis
{
    Task<T?> GetData<T>(string? key);
    Task SetData<T>(string? key, T data);
}