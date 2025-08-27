

using Microsoft.EntityFrameworkCore;

namespace ChefsRegistry.Models
{
    [Keyless]
    public class RegistryViewModel
    {
        public ChefModel? Chef { get; set; }
        public AddressModel? Address { get; set; }
        public PhoneModel? Phone { get; set; }
        public RestaurantModel? Restaurant { get; set; }

    }
}
