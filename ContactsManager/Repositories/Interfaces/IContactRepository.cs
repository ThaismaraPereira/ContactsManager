using ContactsManager.Models;

namespace ContactsManager.Repositories.Interfaces
{
    public interface IContactRepository
    {
         Task<List<ContactModel>> GetAllContacts();
         Task<ContactModel> GetContactById(int Id);
         Task<ContactModel> AddContact(ContactModel contact);
         Task<ContactModel> UpdateContact(ContactModel contact, int id);
         Task<bool> DeleteContact(int id);
    }
}
