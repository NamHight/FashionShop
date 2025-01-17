using FashionShop_API.Dto.RequestDto;
using Microsoft.AspNetCore.SignalR;

namespace FashionShop_API.Services.Chating;

public class ServiceChat : Hub
{
    private readonly ServiceShareData _serviceShareData;

    public ServiceChat(ServiceShareData serviceShareData)
    {
        _serviceShareData = serviceShareData;
    }
    public async Task JoinChat(RequestGuestDto guest)
    {
        await Clients.All.SendAsync("Supporter", "Admin",$"{guest.Username}");
    }
    public async Task JoinSpecificChatRoomSupporter(RequestGuestDto guest)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, guest.Chatroom);
        _serviceShareData.Connections[Context.ConnectionId] = guest;
        await Clients.Group(guest.Chatroom).SendAsync("JoinSpecificChatRoomSupporter", "Hello, Welcome to FashionShop. How Can I help you?", guest.Username);
        
    }
    public async Task SendMessage(string message)
    {
        if (_serviceShareData.Connections.TryGetValue(Context.ConnectionId, out RequestGuestDto requestGuestDto))
        {
            await Clients.Group(requestGuestDto.Chatroom)
                .SendAsync("ReceiveSpecificSupporter",requestGuestDto.Username, message);
        }
    }
}