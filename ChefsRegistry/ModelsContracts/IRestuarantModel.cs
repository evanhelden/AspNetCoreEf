using System.ComponentModel.DataAnnotations;

namespace ChefsRegistry.ModelsContracts
{
    public interface IRestaurantModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int PhoneID { get; set; }
        public int AddressID { get; set; }
        public int ChefID   { get; set; }
    }
}
