namespace FashionShop_API.Services.Views
{
    public interface IServiceView
    {
        Task AddViewAsync(long productId, string sessionId = null, long? customerId = null);
    }
}
