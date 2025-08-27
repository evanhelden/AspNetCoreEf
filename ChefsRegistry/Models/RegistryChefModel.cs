using Microsoft.EntityFrameworkCore;


namespace ChefsRegistry.Models
{
    [Keyless]
    public class RegistryChefModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ChefNumber { get; set; }
        public string Name { get; set; } // Restaurant name
        public string StreetAddress { get; set; }
        public string CityTownVillage { get; set; }
        public string PostalZipCode { get; set; }
        public string StateProvinceRegion { get; set; }
        public string Number { get; set; } // Phone number

    }
}
