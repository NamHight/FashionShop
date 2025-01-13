using FashionShop.Context;
using FashionShop.Models;
using FashionShop.Repositories.Categories;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.Contacts
{
    public class ContactRepository : GenericRepo<Contact>, IContactRepository
    {
        public ContactRepository(MyDbContext context) : base(context)
        {
        }

        public async Task<List<Contact>> GetAllAsync(bool trackChanges)
        {
            var contacts = await FindAll(trackChanges)
                .ToListAsync(); // Lấy dữ liệu từ cơ sở dữ liệu trước

            // Sắp xếp danh sách theo thứ tự quan trọng
            contacts = contacts
                .OrderBy(c => GetStatusPriority(c.Status)) // Gọi hàm để lấy độ ưu tiên
                .ToList();

            return contacts;
        }
        private int GetStatusPriority(string status)
        {
            return status switch
            {
                "importain" => 1,
                "pending" => 2,
                "resovle" => 3,
                _ => 4 // Trạng thái không xác định
            };
        }

        public async Task<Contact?> GetByIdAsync(long id, bool trackChanges)
        {
            var contact = await FindById(item => item.ContactId == id, trackChanges).FirstOrDefaultAsync();
            return contact;
        }
        
        public void Edit(Contact ct)
        {
            Update(ct);
        }
        public async Task<bool> DeleteAsync(long id, bool trackChanges)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                //await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
