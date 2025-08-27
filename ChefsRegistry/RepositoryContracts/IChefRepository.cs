using ChefsRegistry.Models;
using System;

namespace ChefsRegistry.RepositoryContracts
{
    public interface IChefRepository
    {
        IEnumerable<ChefModel> GetAll();
        ChefModel GetById(int id);
        void Add(ChefModel chef);
        void Update(ChefModel chef);
        void Delete(int id);
        void AddChef(ChefModel chefModel);
    }
}
