using System.ComponentModel.DataAnnotations;

namespace ChefsRegistry.ModelsContracts
{
    public interface IPhoneModel
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public int ChefID { get; set; }
        public int RestaurantID { get; set; }
    }
}
