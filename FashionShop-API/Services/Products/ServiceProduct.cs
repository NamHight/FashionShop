using AutoMapper;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Services.Products;
using FashionShop_API.Services.ServiceLogger;


namespace FashionShop.Services.Products
{
    public class ServiceProduct : IServiceProduct
    {
        private readonly IRepositoryManager _managerRepository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public ServiceProduct(IRepositoryManager managerRepository, IMapper mapper, ILoggerManager loggerManager)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
            _loggerManager = loggerManager;
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

        public async Task<IEnumerable<ResponseProductDto>> FindProductsByCategoryIdAsync(long categoryId, bool trackChanges)
        {
            try
            {
                var products = await _managerRepository.Product.GetListProductByCategoryId(categoryId, trackChanges);

                if (products == null || !products.Any())
                {
                    return Enumerable.Empty<ResponseProductDto>();
                }
                var productDtos = _mapper.Map<IEnumerable<ResponseProductDto>>(products);
                return productDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }
		public async Task<Product> GetProductDetailsAsync(ParamCategoryProductDto param)
		{
			// Gọi repository để lấy thông tin chi tiết sản phẩm
			return await _managerRepository.Product.GetProductDetailsAsync(param.CategorySlug, param.ProductSlug);
		}

        public async Task<IEnumerable<ResponseProductDto>> SearchProductsByNameAsync(RequestSearchProductDto requestSearchProductDto)
        {
            var result = await _managerRepository.Product.SearchByName(requestSearchProductDto.searchTerm, requestSearchProductDto.sortOrder);
            var productDtos = _mapper.Map<IEnumerable<ResponseProductDto>>(result);
            return productDtos;
        }
    }
}
