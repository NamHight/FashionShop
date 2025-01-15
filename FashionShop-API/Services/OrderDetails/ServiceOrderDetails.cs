using AutoMapper;
using FashionShop_API.Context;
using FashionShop_API.Models;
using FashionShop_API.Repositories;

namespace FashionShop_API.Services.OrderDetails
{
    public class ServiceOrderDetails : IServiceOrderDetails
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mappers;
        public ServiceOrderDetails(IRepositoryManager repositoryManager, IMapper mappers)
        {
            _repositoryManager = repositoryManager;
            _mappers = mappers;
        }
        public async Task AddOrderDetail(Ordersdetail orderDetail)
        {
           await _repositoryManager.OrderDetails.AddOrderDetail(orderDetail);
            await _repositoryManager.SaveChanges();
        }

    }
}
