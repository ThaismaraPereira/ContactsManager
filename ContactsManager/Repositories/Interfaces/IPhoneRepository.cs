using ContactsManager.Models;

namespace ContactsManager.Repositories.Interfaces
{
    public interface IPhoneRepository
    {
         Task<List<PhoneModel>> GetAllPhones();
         Task<PhoneModel> GetPhoneById(int PhoneId);
        Task<List<PhoneModel>> GetPhonesByContactId(int ContactId);
        Task<PhoneModel> AddPhone(PhoneModel Phone);
         Task<PhoneModel> UpdatePhone(PhoneModel Phone, int PhoneId);
         Task<bool> DeletePhone(int PhoneId);
    }
}
