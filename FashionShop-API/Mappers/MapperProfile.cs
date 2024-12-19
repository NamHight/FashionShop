using AutoMapper;
using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Models;

namespace FashionShop_API.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, ReponseCategoryDto>()
            .ReverseMap();
        CreateMap<Category, RequestCategoryDto>()
            .ReverseMap();
    }
}