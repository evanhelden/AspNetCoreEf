using System.ComponentModel.DataAnnotations;

namespace ChefsRegistry.ModelsContracts
{
    public interface IAddressModel
    {
        public int ID { get; set; }
        public string StreetAddress { get; set; }
        public string CityTownVillage { get; set; }
        public string PostalZipCode { get; set; }
        public string StateProvinceRegion { get; set; }
        public int ChefID { get; set; }
        public int RestaurantID { get; set; }
    }
}
