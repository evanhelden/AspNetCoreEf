using ChefsRegistry.Models;
using ChefsRegistry.RepositoryContracts;
using System.Net;

namespace ChefsRegistry.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly AppDbContext _context;

        public RestaurantRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RestaurantModel> GetAll()
        {
            return _context.Restaurant.ToList();
        }

        public RestaurantModel GetById(int id)
        {
            return _context.Restaurant.Find(id);
        }

        public void Add(RestaurantModel Restaurant)
        {
            _context.Restaurant.Add(Restaurant);
            _context.SaveChanges();
        }

        public void Update(RestaurantModel Restaurant)
        {
            _context.Restaurant.Update(Restaurant);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var Restaurant = _context.Restaurant.Find(id);
            if (Restaurant != null)
            {
                _context.Restaurant.Remove(Restaurant);
                _context.SaveChanges();
            }
        }

        public void AddRestaurant(RestaurantModel Restaurant)
        {
            _context.Restaurant.Add(Restaurant);
            _context.SaveChanges();
        }
    }
}
