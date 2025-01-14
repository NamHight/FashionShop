using AutoMapper;
using FashionShop_API.Repositories;
using FashionShop_API.Services.ServiceLogger;

namespace FashionShop_API.Services.Views
{
    public class ServiceView:IServiceView
    {
        private readonly IRepositoryManager _managerRepository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public ServiceView(IRepositoryManager managerRepository, IMapper mapper, ILoggerManager loggerManager)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }
        public async Task AddViewAsync(long productId, string sessionId = null, long? customerId = null)
        {
            var hasViewed = await _managerRepository.Views.HasUserViewedProductAsync(productId, sessionId, customerId);

            if (!hasViewed)
            {
                await _managerRepository.Views.AddViewAsync(productId, sessionId, customerId);
            }
        }
    }
}
