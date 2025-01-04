using AutoMapper;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Services.Products;
using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.ServiceManager;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Services.Products
{
    public class ServiceProduct : IServiceProduct
    {
        private readonly IRepositoryManager _managerRepository;
    
        private readonly IMapper _mapper;

        public ServiceProduct(IRepositoryManager managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetAllAsync(bool trackChanges)
        {
            var listProduct = await _managerRepository.Product.GetAllAsync(trackChanges);
            return listProduct;
        }

        public async Task<Product?> FindProductByIdAsync(long id, bool trackChanges)
        {
            var product = await _managerRepository.Product.GetByIdAsync(id, trackChanges);
            return product;
        }
    }
}
