using ChefsRegistry.Models;

namespace ChefsRegistry.RepositoryContracts
{
    public interface IPhoneRepository
    {
        IEnumerable<PhoneModel> GetAll();
        PhoneModel GetById(int id);
        void Add(PhoneModel phone);
        void Update(PhoneModel phone);
        void Delete(int id);
        void AddPhone(PhoneModel phoneModel);
    }
}
