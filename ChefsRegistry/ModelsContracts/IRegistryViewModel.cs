using ChefsRegistry.Models;

namespace ChefsRegistry.ModelsContracts
{
    public interface IRegistryViewModel
    {
        public ChefModel? Chef { get; set; }
        public AddressModel? Address { get; set; }
        public PhoneModel? Phone { get; set; }
        public RestaurantModel? Restaurant { get; set; }
    }
}
