using FashionShop.Models;

namespace FashionShop.Repositories.Contacts
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync(bool trackChanges);
        Task<Contact?> GetByIdAsync(long id, bool trackChanges);
        void Edit(Contact ct);
        Task<bool> DeleteAsync(long id, bool trackChanges);

    }
}
