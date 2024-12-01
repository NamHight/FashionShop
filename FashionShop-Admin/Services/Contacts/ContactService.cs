using FashionShop.Models;
using FashionShop.Repositories.ManagerRepository;

namespace FashionShop.Services.Contacts
{
    public class ContactService:IContactService
    {
        private readonly IManagerRepository _managerRepository;
        public ContactService(IManagerRepository managerRepository) => _managerRepository = managerRepository;

        public async Task<List<Contact>> GetAllAsync(bool trackChanges)
        {
            var contacts = await _managerRepository.Contact.GetAllAsync(trackChanges);
            return contacts;
        }
    }
}
