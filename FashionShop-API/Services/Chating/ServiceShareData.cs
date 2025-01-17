using System.Collections.Concurrent;
using FashionShop_API.Dto.RequestDto;

namespace FashionShop_API.Services.Chating;

public class ServiceShareData()
{
    private readonly ConcurrentDictionary<string, RequestGuestDto> _connections = new ConcurrentDictionary<string, RequestGuestDto>();
    
    public ConcurrentDictionary<string, RequestGuestDto> Connections => _connections;
}