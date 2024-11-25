using AutoMapper;
using FashionShop_API.Context;
using FashionShop_API.Dto;
using FashionShop_API.Exceptions;
using FashionShop_API.Models;
using FashionShop_API.Repositories.RepositoryManager;
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

}