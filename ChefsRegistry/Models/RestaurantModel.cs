using ChefsRegistry.ModelsContracts;
using System.ComponentModel.DataAnnotations;

namespace ChefsRegistry.Models
{
    public class RestaurantModel : IRestaurantModel
    {
        public RestaurantModel() { }

        [Display(Name = "Identifier")]
        public int ID { get; set; }
        [Display(Name = "Restaurant Name")]
        [Required]
        public string? Name { get; set; }
        [Display(Name = "Phone Number")]
        public int PhoneID { get; set; }
        [Display(Name = "Address")]
        public int AddressID { get; set; }
        [Display(Name = "Chef")]
        public int ChefID { get; set; }

    }
}
