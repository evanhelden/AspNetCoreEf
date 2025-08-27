using ChefsRegistry.Models;

namespace ChefsRegistry.RepositoryContracts
{
    public interface IRestaurantRepository
    {
        IEnumerable<RestaurantModel> GetAll();
        RestaurantModel GetById(int id);
        void Add(RestaurantModel Restaurant);
        void Update(RestaurantModel Restaurant);
        void Delete(int id);
        void AddRestaurant(RestaurantModel RestaurantModel);

    }
}
