using AutoMapper;
using FashionShop_API.Dto.ResponseDto;
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
using FashionShop_API.Services.Googles;
using FashionShop_API.Services.WebsiteInfos;
using FashionShop_API.Services.Reviews;
using Microsoft.Extensions.Options;
using FashionShop_API.Services.Articles;
using FashionShop_API.Services.Orders;
using FashionShop_API.Services.Suppliers;
using FashionShop_API.Services.Views;

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
	private readonly Lazy<IServiceGoogle> _google;
    private readonly Lazy<IServicePromotion> _promotion;
    private readonly Lazy<IServiceArticle> _article;
    private readonly Lazy<IServiceOrders> _order;
    private readonly Lazy<IServiceSuppiler> _suppiler;
    private readonly Lazy<IServiceView> _views;
    public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper,ILoggerManager logger,IConfiguration configuration,IOptions<GoogleOption> googleOption)
    {
        _repositoryManager = repositoryManager;
        _category = new Lazy<IServiceCategory>(() => new ServiceCategory(repositoryManager,mapper,logger));
        _authenticate = new Lazy<IServiceAuthenticate>(() => new ServiceAuthenticate(repositoryManager,mapper,logger,configuration));
        _customer = new Lazy<IServiceCustomer>(() => new ServiceCustomer(mapper,repositoryManager));
        _product = new Lazy<IServiceProduct>(() => new ServiceProduct(repositoryManager, mapper,logger));
        _favorite = new Lazy<IServiceFavorites>(() => new ServiceFavorites(mapper,repositoryManager));
        _contact = new Lazy<IServiceContact>(() => new ServiceContact(repositoryManager, logger, mapper));
        _webisteInfo = new Lazy<IServiceWebsiteInfo>(() => new ServiceWebsiteInfo(repositoryManager, logger, mapper));
        _review = new Lazy<IServiceReviews>(() => new ServiceReviews(repositoryManager, mapper));
        _google = new Lazy<IServiceGoogle>(() => new ServiceGoogle(repositoryManager,googleOption,logger,mapper));
        _promotion = new Lazy<IServicePromotion>(() => new ServicePromotion(repositoryManager, mapper));
        _article = new Lazy<IServiceArticle>(() => new ServiceArticle(repositoryManager, mapper));
        _order = new Lazy<IServiceOrders>(() => new ServiceOrders(repositoryManager, mapper));
        _suppiler = new Lazy<IServiceSuppiler>(()=> new ServiceSuppiler(repositoryManager, mapper));
        _views = new Lazy<IServiceView>(() => new ServiceView(repositoryManager, mapper, logger));
    }
    public IServiceCategory Category => _category.Value;
    public IServiceAuthenticate Authenticate => _authenticate.Value;
    public IServiceCustomer Customer => _customer.Value;
    public IServiceProduct Product => _product.Value;
    public IServiceFavorites Favorite => _favorite.Value;
    public IServiceContact Contact => _contact.Value;
    public IServiceWebsiteInfo WebsiteInfo => _webisteInfo.Value;
    public IServiceGoogle Google => _google.Value;
    public IServiceReviews Review => _review.Value;
    public IServicePromotion Promotion => _promotion.Value;
    public IServiceArticle Article => _article.Value;
    public IServiceOrders Orders => _order.Value;
    public IServiceSuppiler Suppiler => _suppiler.Value;
    public IServiceView Views => _views.Value;
}