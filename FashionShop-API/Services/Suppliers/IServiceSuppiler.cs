
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
namespace FashionShop_API.Services.Suppliers
{
    public interface IServiceSuppiler
    {
        Task<List<Supplier>> GetAllAsync(bool trackChanges);
    }
}
