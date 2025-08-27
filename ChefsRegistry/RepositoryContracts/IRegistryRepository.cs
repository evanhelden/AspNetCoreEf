using ChefsRegistry.Models;

namespace ChefsRegistry.RepositoryContracts
{
    public interface IRegistryRepository
    {

        void Add(RegistryViewModel chef);

        void AddUDT(RegistryViewModel chef);

        public RegistryChefModel GetChef(int ChefID);

        public void UpdateChef(RegistryChefModel cm, int ID);
    }
}
