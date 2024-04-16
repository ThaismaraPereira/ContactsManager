using ContactsManager.Models;

namespace ContactsManager.Repositories.Interfaces
{
    public interface IEmailRepository
    {
         Task<List<EmailModel>> GetAllEmails();
         Task<EmailModel> GetEmailById(int EmailId);
         Task<List<EmailModel>> GetEmailsByContactId(int ContactId);
         Task<EmailModel> AddEmail(EmailModel Email);
         Task<EmailModel> UpdateEmail(EmailModel Email, int EmailId);
         Task<bool> DeleteEmail(int EmailId);
    }
}
