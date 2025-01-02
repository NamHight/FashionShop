using FashionShop_API.Models;

namespace FashionShop_API.Repositories.Contacts
{
    public interface IRepositoryContact
    {
        Task CreateAsync(Contact contact);
    }
}
