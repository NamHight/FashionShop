using AutoMapper;
using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Models;

namespace FashionShop_API.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, ResponseCategoryDto>()
            .ReverseMap();
        CreateMap<Category, RequestCategoryDto>()
            .ReverseMap();
        CreateMap<Customer, RequestAuthenticateRegisterDto>()
            .ReverseMap();
        CreateMap<Customer, ResponseCustomerDto>()
            .ReverseMap();
        CreateMap<Contact, RequestContactDto>()
            .ReverseMap();
    }
}