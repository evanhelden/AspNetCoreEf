using ChefsRegistry.Models;

namespace ChefsRegistry.RepositoryContracts
{
    public interface IAddressRepository
    {
        IEnumerable<AddressModel> GetAll();
        AddressModel GetById(int id);
        void Add(AddressModel chef);
        void Update(AddressModel chef);
        void Delete(int id);
        void AddAddress(AddressModel addressModel);
    }
}
