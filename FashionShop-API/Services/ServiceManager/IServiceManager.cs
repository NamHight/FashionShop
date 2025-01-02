﻿using FashionShop_API.Services.Authenticates;
using FashionShop_API.Services.Categories;
using FashionShop_API.Services.Contacts;
using FashionShop_API.Services.Customers;
using FashionShop_API.Services.Promotions;

namespace FashionShop_API.Services.ServiceManager;

public interface IServiceManager
{
     IServiceCategory Category { get; }
     IServiceAuthenticate Authenticate { get; }
     IServiceCustomer Customer { get; }
     IServiceContact Contact { get; }
     IServicePromotion Promotion { get; }
}