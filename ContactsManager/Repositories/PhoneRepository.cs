using ContactsManager.Data;
using ContactsManager.Models;
using ContactsManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ContactsManager.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly ContactsManagerContext _dbContext;
        public PhoneRepository(ContactsManagerContext ContactsManagerContext) { 
            _dbContext = ContactsManagerContext;
        }

        public async Task<List<PhoneModel>> GetAllPhones()
        {
            return await _dbContext.Phones
                .ToListAsync();
        }

        public async Task<PhoneModel> GetPhoneById(int PhoneId)
        {
            return await _dbContext.Phones
                .FirstOrDefaultAsync(x => x.PhoneId == PhoneId );
        }

        public async Task<List<PhoneModel>> GetPhonesByContactId(int ContactId)
        {
            return await _dbContext.Phones
            .Where(x => x.ContactId == ContactId)
            .ToListAsync();
        }

        public async Task<PhoneModel> AddPhone(PhoneModel Phone)
        {
            await _dbContext.Phones.AddAsync(Phone);
            await _dbContext.SaveChangesAsync();

            return Phone;
        }
        public async Task<PhoneModel> UpdatePhone(PhoneModel Phone, int PhoneId)
        {
            PhoneModel PhoneById = await GetPhoneById(PhoneId);

            if (PhoneById == null)
            {
                throw new Exception($"Telefone de ID {PhoneId} não localizado!");
            }

            PhoneById.ContactId = Phone.ContactId;
            PhoneById.PhoneNumber = Phone.PhoneNumber;
            PhoneById.PhoneNumberType = Phone.PhoneNumberType;
            _dbContext.Phones.Update(PhoneById);
            await _dbContext.SaveChangesAsync();

            return Phone;
        }

        public async Task<bool> DeletePhone(int PhoneId)
        {
            PhoneModel PhoneById = await GetPhoneById(PhoneId);

            if (PhoneById == null)
            {
                throw new Exception($"Telefone de ID {PhoneId} não localizado!");
            }

            _dbContext.Phones.Remove(PhoneById);
            await _dbContext.SaveChangesAsync();

            return true;
        }       
    }
}
