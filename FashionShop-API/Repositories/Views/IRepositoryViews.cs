namespace FashionShop_API.Repositories.Views
{
    public interface IRepositoryViews
    {
        Task<bool> HasUserViewedProductAsync(long productId, string sessionId = null, long? customerId = null);
        Task AddViewAsync(long productId, string sessionId = null, long? customerId = null);
    }
}
