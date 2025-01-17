using AutoMapper;
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
        CreateMap<RequestOrderDto, Order>()
            .ReverseMap();
        CreateMap<Ordersdetail, ResponseOrderDetailsDto>()
            .ForMember(item => item.ProductName, ots => ots.MapFrom(item => item.Product.ProductName))
            .ForMember(item => item.Banner, ots => ots.MapFrom(item => item.Product.Banner))
            .ForMember(item => item.Price, ots => ots.MapFrom(item => item.Product.Price))
            .ForMember(item => item.CategoryName, ots => ots.MapFrom(item => item.Product.Category.CategoryName))
            .ReverseMap();
        CreateMap<Customer, ResponseOrdersDto>()
            .ReverseMap();
        //CreateMap<Product, ResponseOrderDetailsDto>()
        //   .IncludeMembers(item => item.Category)
        //    .ReverseMap();
        //CreateMap<Category, ResponseOrderDetailsDto>().ReverseMap();

        CreateMap<Favorite, RequestFarvoriteDto>()
            .ReverseMap();
        CreateMap<Product, ResponseProductDto>()
            .ReverseMap();
        CreateMap<Product, ResponseSearchProductDto>()
            .IncludeMembers(e => e.Category)
            .ReverseMap();
        CreateMap<Category, ResponseSearchProductDto>()
            .ReverseMap();

        CreateMap<Supplier, SuppilerDto>().ReverseMap();

        CreateMap<Order, RequestOrderCancelDto>().ReverseMap();
    }
}