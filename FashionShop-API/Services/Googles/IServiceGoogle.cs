using FashionShop_API.Dto;
using FashionShop_API.Dto.ResponseDto;

namespace FashionShop_API.Services.Googles;

public interface IServiceGoogle
{
    Task<ResponseCustomerDto?> GoogleSignIn(GoogleSignVM googleSignVM);
}