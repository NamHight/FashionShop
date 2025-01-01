using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;


namespace FashionShop_API.Services.Caching;

public class ServiceCacheRedis : IServiceCacheRedis
{
    private readonly IDistributedCache? _cache;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    public ServiceCacheRedis(IDistributedCache? cache, IConnectionMultiplexer connectionMultiplexer)
    {
        _cache = cache;
        _connectionMultiplexer = connectionMultiplexer;
    }
    public bool CheckConnection()
    {
        try
        {
            return _connectionMultiplexer.IsConnected;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<T?> GetData<T>(string? key)
    {
        var data = await _cache?.GetStringAsync(key);
        if (data is null)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(data)!;
    }
    
    public async Task SetData<T>(string? key, T data)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(data),options);
    }
}