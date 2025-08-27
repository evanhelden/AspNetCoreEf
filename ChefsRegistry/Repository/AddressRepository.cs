using ChefsRegistry.Models;
using ChefsRegistry.RepositoryContracts;
using System.Net;

namespace ChefsRegistry.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AddressModel> GetAll()
        {
            return _context.Address.ToList();
        }

        public AddressModel GetById(int id)
        {
            return _context.Address.Find(id);
        }

        public void Add(AddressModel address)
        {
            _context.Address.Add(address);
            _context.SaveChanges();
        }

        public void Update(AddressModel address)
        {
            _context.Address.Update(address);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var address = _context.Address.Find(id);
            if (address != null)
            {
                _context.Address.Remove(address);
                _context.SaveChanges();
            }
        }

        public void AddAddress(AddressModel address)
        {
            _context.Address.Add(address);
            _context.SaveChanges();
        }
    }
}
