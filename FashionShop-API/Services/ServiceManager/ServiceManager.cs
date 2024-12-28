using AutoMapper;
using FashionShop_API.Repositories.RepositoryManager;
using FashionShop_API.Services.Categories;
using FashionShop_API.Services.ServiceAuthenticate;
using FashionShop_API.Services.ServiceLogger;

namespace FashionShop_API.Services.ServiceManager;

public class ServiceManager : IServiceManager
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly Lazy<IServiceCategory> _category;
    private readonly Lazy<IServiceAuthenticate> _authenticate;
    public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper,ILoggerManager logger,IConfiguration configuration)
    {
        _repositoryManager = repositoryManager;
        _category = new Lazy<IServiceCategory>(() => new ServiceCategory(repositoryManager,mapper,logger));
        _authenticate = new Lazy<IServiceAuthenticate>(() => new ServiceAuthenticate.ServiceAuthenticate(repositoryManager,mapper,logger,configuration));
    }
    public IServiceCategory Category => _category.Value;
    public IServiceAuthenticate Authenticate => _authenticate.Value;
}