using AutoMapper;
using FashionShop_API.Context;
using FashionShop_API.Dto;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Exceptions;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Repositories.Shared;
using FashionShop_API.Services.ServiceLogger;

namespace FashionShop_API.Services.Categories;

public class ServiceCategory  : IServiceCategory
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public ServiceCategory(IRepositoryManager repositoryManager,IMapper mapper,ILoggerManager logger)
    {
        this._repositoryManager = repositoryManager;
        this._logger = logger;
        this._mapper = mapper;
    }
    public async Task<IEnumerable<ResponseCategoryChildrenDto>> GetAllCategoryAsync(bool trackChanges)
    {
        try
        {
            var categories = await _repositoryManager.Category.GetAllCategoriesAsync(trackChanges);
            if(categories is null) throw new CategoryListNotFoundException("Categories not found");
            return categories;
        }
        catch (Exception e)
        {
            _logger.LogError( "Service Category: " + nameof(GetAllCategoryAsync) + " Error: " +e.Message);
            throw new Exception(e.Message);
        }
       
    }

    public async Task<ResponseCategoryDto?> GetCategoryByIdAsync(long id, bool trackChanges)
    {
            var category = await _repositoryManager.Category.GetCategoryByIdAsync(id, trackChanges);
            if (category is null) throw new CategoryNotFoundException(id);
            var categoryDto = _mapper.Map<ResponseCategoryDto>(category);
            return categoryDto;
    }

    public async Task<(IEnumerable<ResponseCategoryDto> data,PageInfo meta)> GetAllPaginatedAsync(int page, int limit)
    {
        var categories = await _repositoryManager.Category.GetAllPaginatedAsync(page, limit);
        var categoriesDto = _mapper.Map<IEnumerable<ResponseCategoryDto>>(categories);
        return (data: categoriesDto, meta: categories.PageInfo);
    }

    public async Task<(IEnumerable<ResponseCategoryDto> data ,PageInfo meta)> GetAllPaginatedAndSearchAndSortAsync(ParamCategoryDto paramCategoryDto)
    {
        var categories = await _repositoryManager
            .Category
            .GetAllPaginatedAndSearchAndSortAsync(paramCategoryDto.SearchTerm, paramCategoryDto.Page, paramCategoryDto.Limit, paramCategoryDto.SortBy, paramCategoryDto.SortOrder);
        var categoriesDto = _mapper.Map<IEnumerable<ResponseCategoryDto>>(categories);
        return (data: categoriesDto,meta:categories.PageInfo);
    }

    public async Task<RequestCategoryDto> CreateAsync(RequestCategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        category.CreatedAt = DateTime.UtcNow;
        if (await CheckCategorySlugExistsAsync(category.Slug))
        {
            throw new SlugExistedException(category.Slug);
        }
        await _repositoryManager.Category.CreateAsync(category);
        await _repositoryManager.SaveChanges();
        var categoryReturn = _mapper.Map<RequestCategoryDto>(category);
        _logger.LogInfo("Service Category: " + nameof(CreateAsync) + " Success");
        return categoryReturn;
    }

    private async Task<bool> CheckCategorySlugExistsAsync(string slug)
    {
        return await _repositoryManager.Category.CheckCategorySlugExistsAsync(slug);
    }
}