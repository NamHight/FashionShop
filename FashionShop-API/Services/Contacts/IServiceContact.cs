using FashionShop_API.Dto.RequestDto;

namespace FashionShop_API.Services.Contacts
{
    public interface IServiceContact
    {
        Task<RequestContactDto> CreateAsync(RequestContactDto request);
    }
}
