using AutoMapper;
using FashionShop_API.Repositories.RepositoryManager;
using FashionShop_API.Services.Authenticates;
using FashionShop_API.Services.Categories;
using FashionShop_API.Services.Customers;
using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.Favorites;

namespace FashionShop_API.Services.ServiceManager;

public class ServiceManager : IServiceManager
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly Lazy<IServiceCategory> _category;
    private readonly Lazy<IServiceAuthenticate> _authenticate;
    private readonly Lazy<IServiceCustomer> _customer;
    private readonly Lazy<IServiceFavorites> _favorite;
    public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper,ILoggerManager logger,IConfiguration configuration)
    {
        _repositoryManager = repositoryManager;
        _category = new Lazy<IServiceCategory>(() => new ServiceCategory(repositoryManager,mapper,logger));
        _authenticate = new Lazy<IServiceAuthenticate>(() => new ServiceAuthenticate(repositoryManager,mapper,logger,configuration));
        _customer = new Lazy<IServiceCustomer>(() => new ServiceCustomer(mapper,repositoryManager));
        _favorite = new Lazy<IServiceFavorites>(() => new ServiceFavorites(mapper,repositoryManager));
    }
    public IServiceCategory Category => _category.Value;
    public IServiceAuthenticate Authenticate => _authenticate.Value;
    public IServiceCustomer Customer => _customer.Value;
    public IServiceFavorites Favorite => _favorite.Value;
}