using FashionShop.Models;

namespace FashionShop.Services.Contacts
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync(bool trackChanges);
    }
}
