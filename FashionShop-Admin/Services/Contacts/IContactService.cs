using FashionShop.Models;

namespace FashionShop.Services.Contacts
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync(bool trackChanges);
        Task<Contact?> GetByIdAsync(long id, bool trackChanges);
        Task UpdateStatusAsync(long id, string status, bool trackChanges);
        Task EditAsync(Contact ct, bool trackChanges);
        Task<bool> DeleteAsync(long id, bool trackChanges);
    }
}
