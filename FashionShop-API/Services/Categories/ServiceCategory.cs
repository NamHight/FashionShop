using AutoMapper;
using FashionShop_API.Context;
using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Exceptions;
using FashionShop_API.Models;
using FashionShop_API.Repositories.RepositoryManager;
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
    public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync(bool trackChanges)
    {
        try
        {
            var categories = await _repositoryManager.Category.GetAllCategoriesAsync(trackChanges);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        }
        catch (Exception e)
        {
            _logger.LogError( "Service Category: " + nameof(GetAllCategoryAsync) + " Error: " +e.Message);
            throw new Exception(e.Message);
        }
       
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(long id, bool trackChanges)
    {
            var category = await _repositoryManager.Category.GetCategoryByIdAsync(id, trackChanges);
            if (category is null) throw new CategoryNotFoundException(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
    }

    public async Task<ResponsePage<CategoryDto>> GetAllPaginatedAsync(int page, int limit)
    {
        var categories = await _repositoryManager.Category.GetAllPaginatedAsync(page, limit);
        var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
        return ResponsePage<CategoryDto>
            .Builder
            .Empty()
            .SetData(categoriesDto.ToList())
            .SetCurrentPage(categories.PageInfo.CurrentPage)
            .SetPageSize(categories.PageInfo.PageSize)
            .SetTotalPages(categories.PageInfo.TotalPages)
            .SetHasNext(categories.PageInfo.HasNextpage)
            .SetHasPrevious(categories.PageInfo.HasPreviouspage)
            .Build();
    }

    public async Task<RequestCategoryDto> CreateAsync(RequestCategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        category.CreatedAt = DateTime.Now;
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