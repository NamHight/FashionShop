﻿using AutoMapper;
using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;

namespace FashionShop_API.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, ResponseCategoryDto>()
            .ReverseMap();
        CreateMap<Customer, RequestAuthenticateRegisterDto>()
            .ReverseMap();
        CreateMap<Customer, ResponseCustomerDto>()
            .ReverseMap();
        CreateMap<Contact, RequestContactDto>()
            .ReverseMap();
        CreateMap<Favorite, ResponseFavoritesDto>()
            .IncludeMembers(item => item.Product)
           .ReverseMap();
        CreateMap<Product, ResponseFavoritesDto>()
            .IncludeMembers(item => item.Category).ReverseMap();
        CreateMap<Category, ResponseFavoritesDto>()
           .ReverseMap();
		CreateMap<Review, RequestReviewDto>()
			.ReverseMap();
		CreateMap<Review, ResponseReviewDto>()
			.ReverseMap();
        CreateMap<Promotion, ResponsePromotionDto>()
            .ReverseMap();
        CreateMap<Article, ResponseArticleDto>()
            .ReverseMap();

        CreateMap<Order, ResponseOrdersDto>()
            .IncludeMembers(item => item.Customer)
            .ReverseMap();
        CreateMap<Ordersdetail, ResponseOrderDetailsDto>()
            .ReverseMap();
        CreateMap<Product, ResponseOrdersDto>()
            .IncludeMembers(item => item.Category).ReverseMap();
        CreateMap<Customer, ResponseOrdersDto>()
            .ReverseMap();
        CreateMap<Category, ResponseOrdersDto>().ReverseMap();
    }
}