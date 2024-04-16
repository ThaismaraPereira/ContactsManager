using ContactsManager.Data;
using ContactsManager.Models;
using ContactsManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsManagerContext _dbContext;
        public ContactRepository(ContactsManagerContext contactsManagerContext) { 
            _dbContext = contactsManagerContext;
        }

        public async Task<List<ContactModel>> GetAllContacts()
        {
            return await _dbContext.Contacts
                .Include(x => x.Phones)
                .Include(x => x.Emails)
                .ToListAsync();
        }

        public async Task<ContactModel> GetContactById(int Id)
        {
            ContactModel contact = await _dbContext.Contacts
                .Include(x => x.Phones)
                .Include(x => x.Emails)
                .FirstOrDefaultAsync(x => x.Id == Id);
            if (contact == null)
            {
                throw new KeyNotFoundException($"Contato com ID {Id} não encontrado!");
            }
            return contact;
        }

        public async Task<ContactModel> AddContact(ContactModel contact)
        {
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            return contact;
        }
        public async Task<ContactModel> UpdateContact(ContactModel contact, int id)
        {
            ContactModel contactById = await GetContactById(id);

            if (contactById == null)
            {
                throw new Exception($"Contato de ID {id} não localizado!");
            }

            contactById.Name = contact.Name;
            contactById.CPF = contact.CPF;
            contactById.BirthDay = contact.BirthDay;
            contactById.IsActive = contact.IsActive;
            _dbContext.Contacts.Update(contactById);
            await _dbContext.SaveChangesAsync();

            return contact;
        }

        public async Task<bool> DeleteContact(int id)
        {
            ContactModel contactById = await GetContactById(id);

            if (contactById == null)
            {
                throw new Exception($"Contato de ID {id} não localizado!");
            }

            _dbContext.Contacts.Remove(contactById);
            await _dbContext.SaveChangesAsync();

            return true;
        }       
    }
}
