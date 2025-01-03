using FashionShop_API.Context;
using FashionShop_API.Models;

namespace FashionShop_API.Repositories.Contacts
{
    public class RepositoryContact : RepositoryBase<Contact>, IRepositoryContact
    {
        public RepositoryContact(MyDbContext context): base(context) { }
        public async Task CreateAsync(Contact contact)
        {
            await Create(contact);
        }
    }
}
