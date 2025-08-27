using System.ComponentModel.DataAnnotations;

namespace ChefsRegistry.ModelsContracts
{
    public interface IChefModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ChefNumber { get; set; }
    }
}
