using AutoMapper;
using FashionShop_API.Repositories.RepositoryManager;
using FashionShop_API.Services.Categories;
using FashionShop_API.Services.ServiceLogger;

namespace FashionShop_API.Services.ServiceManager;

public class ServiceManager : IServiceManager
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly Lazy<IServiceCategory> _category;
    
    public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper,ILoggerManager logger)
    {
        _repositoryManager = repositoryManager;
        _category = new Lazy<IServiceCategory>(() => new ServiceCategory(repositoryManager,mapper,logger));
    }




    public IServiceCategory Category => _category.Value;
}