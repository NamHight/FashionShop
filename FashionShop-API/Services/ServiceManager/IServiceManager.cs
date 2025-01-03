﻿using FashionShop_API.Services.Authenticates;
using FashionShop_API.Services.Categories;
using FashionShop_API.Services.Customers;
using FashionShop_API.Services.Favorites;

namespace FashionShop_API.Services.ServiceManager;

public interface IServiceManager
{
     IServiceCategory Category { get; }
     IServiceAuthenticate Authenticate { get; }
     IServiceCustomer Customer { get; }
     IServiceFavorites Favorite { get; }
}