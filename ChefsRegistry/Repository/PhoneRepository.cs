using ChefsRegistry.Models;
using ChefsRegistry.RepositoryContracts;

namespace ChefsRegistry.Repository
{
    public class PhoneRepository : IPhoneRepository
    {

        private readonly AppDbContext _context;

        public PhoneRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PhoneModel> GetAll()
        {
            return _context.Phone.ToList();
        }

        public PhoneModel GetById(int id)
        {
            return _context.Phone.Find(id);
        }

        public void Add(PhoneModel phone)
        {
            _context.Phone.Add(phone);
            _context.SaveChanges();
        }

        public void Update(PhoneModel phone)
        {
            _context.Phone.Update(phone);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var phone = _context.Phone.Find(id);
            if (phone != null)
            {
                _context.Phone.Remove(phone);
                _context.SaveChanges();
            }
        }

        public void AddPhone(PhoneModel phone)
        {
            _context.Phone.Add(phone);
            _context.SaveChanges();
        }

    }
}
