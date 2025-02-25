﻿
using System.Data.SqlTypes;
using FashionShop_API.Dto;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;

namespace FashionShop_API.Services.Products
{
    public interface IServiceProduct
    {
        Task<IEnumerable<ResponseProductDto>> FindProductsByCategoryIdAsync(string slug, bool trackChanges);
        Task<List<Product>> GetAllAsync(bool trackChanges);
        Task<Product?> FindProductByIdAsync( long id, bool trackChanges);
        Task<Product> GetProductDetailsAsync(ParamCategoryProductDto param);
        Task<IEnumerable<ResponseProductDto>> SearchProductsByNameAsync(RequestSearchProductDto requestSearchProductDto);
        Task<int> GetFavoritesCountAsync(long productId);
        Task<int> GetViewsCountAsync(long productId);
        Task<double> GetAverageReviewAsync(long productId);
        Task<(IEnumerable<ResponseSearchProductDto> products, PageInfo pageInfo)> GetProductSearchAndFilterAsync(RequestProductDto requestProductDto, bool trackChanges);
    }
}
