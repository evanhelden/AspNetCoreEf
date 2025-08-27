using ChefsRegistry.ModelsContracts;
using System.ComponentModel.DataAnnotations;

namespace ChefsRegistry.Models
{
    public class AddressModel : IAddressModel
    {

        [Display(Name = "Identifier")]
        public int ID { get; set; }
        [Display(Name = "Street Address")]
        [Required]
        public string StreetAddress { get; set; }
        [Display(Name = "City/Town/Village")]
        [Required]
        public string CityTownVillage { get; set; }
        [Display(Name = "Postal Zip Code")]
        [Required]
        public string PostalZipCode { get; set; }
        [Display(Name = "State Province Region")]
        [Required]
        public string StateProvinceRegion { get; set; }
        [Display(Name = "ChefID")]
        [Required]
        public int ChefID { get; set; }
        [Display(Name = "RestaurantID")]
        [Required]
        public int RestaurantID { get; set; }

    }
}
