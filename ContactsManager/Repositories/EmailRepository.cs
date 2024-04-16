using ContactsManager.Data;
using ContactsManager.Models;
using ContactsManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmailsManager.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ContactsManagerContext _dbContext;
        public EmailRepository(ContactsManagerContext EmailsManagerContext) { 
            _dbContext = EmailsManagerContext;
        }

        public async Task<List<EmailModel>> GetAllEmails()
        {
            return await _dbContext.Emails
                .ToListAsync();
        }

        public async Task<EmailModel> GetEmailById(int EmailId)
        {
            return await _dbContext.Emails
                .FirstOrDefaultAsync(x => x.EmailId == EmailId);
        }

        public async Task<List<EmailModel>> GetEmailsByContactId(int ContactId)
        {
            return await _dbContext.Emails
            .Where(x => x.ContactId == ContactId)
            .ToListAsync();
        }

        public async Task<EmailModel> AddEmail(EmailModel Email)
        {
            await _dbContext.Emails.AddAsync(Email);
            await _dbContext.SaveChangesAsync();

            return Email;
        }
        public async Task<EmailModel> UpdateEmail(EmailModel Email, int EmailId)
        {
            EmailModel EmailById = await GetEmailById(EmailId);

            if (EmailById == null)
            {
                throw new Exception($"Contato de ID {EmailId} não localizado!");
            }

            EmailById.ContactId = Email.ContactId;
            EmailById.EmailAddress = Email.EmailAddress;
            EmailById.EmailAddressType = Email.EmailAddressType;
            _dbContext.Emails.Update(EmailById);
            await _dbContext.SaveChangesAsync();

            return Email;
        }

        public async Task<bool> DeleteEmail(int EmailId)
        {
            EmailModel EmailById = await GetEmailById(EmailId);

            if (EmailById == null)
            {
                throw new Exception($"Contato de ID {EmailId} não localizado!");
            }

            _dbContext.Emails.Remove(EmailById);
            await _dbContext.SaveChangesAsync();

            return true;
        }       
    }
}
