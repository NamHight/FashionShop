using AutoMapper;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Services.Suppliers;
using FashionShop_API.Services.ServiceLogger;
namespace FashionShop_API.Services.Suppliers
{
    public class ServiceSuppiler(IRepositoryManager managerRepository, IMapper mapper) : IServiceSuppiler
    {
        private readonly IRepositoryManager _managerRepository = managerRepository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper = mapper;

        public async Task<List<Supplier>> GetAllAsync(bool trackChanges)
        {
            var listSupplier = await _managerRepository.Suppiler.GetAllAsync(trackChanges);
            return listSupplier;
        }

    }
}
