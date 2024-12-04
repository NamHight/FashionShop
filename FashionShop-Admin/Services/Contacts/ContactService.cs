using FashionShop.Models;
using FashionShop.Repositories.ManagerRepo;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Contact?> GetByIdAsync(long id, bool trackChanges)
        {
            var contact = await _managerRepository.Contact.GetByIdAsync(id,trackChanges);
            return contact;
        }
        public async Task UpdateStatusAsync(long id, string status, bool trackChanges)
        {
           var contact = await _managerRepository.Contact.GetByIdAsync(id,trackChanges);
            if (contact != null)
            {
                contact.Status = status;
                await _managerRepository.SaveAsync();
            }

            
        }

        public async Task<bool> DeleteAsync(long id, bool trackChanges)
        {
            var contact = await _managerRepository.Contact.DeleteAsync(id,trackChanges);
            return contact;
        }
    }
}
