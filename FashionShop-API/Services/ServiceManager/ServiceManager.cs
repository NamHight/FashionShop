using AutoMapper;
using FashionShop.Services.Products;
using FashionShop_API.Repositories;
using FashionShop_API.Services.Authenticates;
using FashionShop_API.Services.Categories;
using FashionShop_API.Services.Contacts;
using FashionShop_API.Services.Customers;
using FashionShop_API.Services.Products;
using FashionShop_API.Services.Promotions;
using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.Favorites;
using FashionShop_API.Services.WebsiteInfos;
using FashionShop_API.Services.Reviews;
using FashionShop_API.Services.Articles;

namespace FashionShop_API.Services.ServiceManager;

public class ServiceManager : IServiceManager
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly Lazy<IServiceCategory> _category;
    private readonly Lazy<IServiceAuthenticate> _authenticate;
    private readonly Lazy<IServiceCustomer> _customer;
    private readonly Lazy<IServiceProduct> _product;
    private readonly Lazy<IServiceFavorites> _favorite;
    private readonly Lazy<IServiceContact> _contact;
    private readonly Lazy<IServiceWebsiteInfo> _webisteInfo;
	private readonly Lazy<IServiceReviews> _review;
    private readonly Lazy<IServicePromotion> _promotion;
    private readonly Lazy<IServiceArticle> _article;
    public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper,ILoggerManager logger,IConfiguration configuration)
    {
        _repositoryManager = repositoryManager;
        _category = new Lazy<IServiceCategory>(() => new ServiceCategory(repositoryManager,mapper,logger));
        _authenticate = new Lazy<IServiceAuthenticate>(() => new ServiceAuthenticate(repositoryManager,mapper,logger,configuration));
        _customer = new Lazy<IServiceCustomer>(() => new ServiceCustomer(mapper,repositoryManager));
        _product = new Lazy<IServiceProduct>(() => new ServiceProduct(repositoryManager, mapper));
        _favorite = new Lazy<IServiceFavorites>(() => new ServiceFavorites(mapper,repositoryManager));
        _contact = new Lazy<IServiceContact>(() => new ServiceContact(repositoryManager, logger, mapper));
        _webisteInfo = new Lazy<IServiceWebsiteInfo>(() => new ServiceWebsiteInfo(repositoryManager, logger, mapper));
        _review = new Lazy<IServiceReviews>(() => new ServiceReviews(repositoryManager, mapper));
        _promotion = new Lazy<IServicePromotion>(() => new ServicePromotion(repositoryManager, mapper));
        _article = new Lazy<IServiceArticle>(() => new ServiceArticle(repositoryManager, mapper));
    }
    public IServiceCategory Category => _category.Value;
    public IServiceAuthenticate Authenticate => _authenticate.Value;
    public IServiceCustomer Customer => _customer.Value;
    public IServiceProduct Product => _product.Value;
    public IServiceFavorites Favorite => _favorite.Value;
    public IServiceContact Contact => _contact.Value;
    public IServiceWebsiteInfo WebsiteInfo => _webisteInfo.Value;
	public IServiceReviews Review => _review.Value;

    public IServicePromotion Promotion => _promotion.Value;
    public IServiceArticle Article => _article.Value;
}