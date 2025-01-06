using AutoMapper;
using FashionShop_API.Context;
using FashionShop_API.Repositories.Categories;
using FashionShop_API.Repositories.Products;
using FashionShop_API.Repositories.Contacts;
using FashionShop_API.Repositories.Customers;
using FashionShop_API.Repositories.Favorites;
using FashionShop_API.Repositories.WebsiteInfos;
using FashionShop_API.Repositories.Reviews;

namespace FashionShop_API.Repositories.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly MyDbContext _context;
    private Lazy<IRepositoryCategory> _category;
    private Lazy<IRepositoryCustomer> _customer;
    private Lazy<IRepositoryProduct> _product;
    private Lazy<IRepositoryContact> _contact;
    private Lazy<IRepositoryFavorites> _favorite;
    private Lazy<IRepositoryWebsiteInfo> _websiteInfo;
	private Lazy<IRepositoryReviews> _review;

    public RepositoryManager (MyDbContext context,IMapper _mapper)
    {
        _context = context;
        _customer = new Lazy<IRepositoryCustomer>(() => new RepositoryCustomer(context));
        _category = new Lazy<IRepositoryCategory>( () => new RepositoryCategory(context,_mapper));
        _contact = new Lazy<IRepositoryContact>(() => new RepositoryContact(context));
        _product = new Lazy<IRepositoryProduct>(() => new RepositoryProduct(context));
        _favorite = new Lazy<IRepositoryFavorites>( () => new RepositoryFavorites(context) );
        _websiteInfo = new Lazy<IRepositoryWebsiteInfo>(() => new RepositoryWebsiteInfo(context));
		_review = new Lazy<IRepositoryReviews>(() => new RepositoryReviews(context));
	}

    public IRepositoryCategory Category => _category.Value;
    public IRepositoryCustomer Customer => _customer.Value;
    public IRepositoryContact Contact => _contact.Value;
    public IRepositoryFavorites Favorite => _favorite.Value;
    public IRepositoryProduct Product => _product.Value;
    public IRepositoryWebsiteInfo WebsiteInfo => _websiteInfo.Value;
	public IRepositoryReviews Review => _review.Value;

	public async Task SaveChanges() => await _context.SaveChangesAsync();
	public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}