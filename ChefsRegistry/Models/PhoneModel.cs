using ChefsRegistry.ModelsContracts;
using System.ComponentModel.DataAnnotations;
using ChefsRegistry.Repository;

namespace ChefsRegistry.Models
{
    public class PhoneModel : IPhoneModel
    {
        [Display(Name = "Identifier")]
        public int ID { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        public string Number { get; set; }
    
        [Display(Name = "Chef")]
        public int ChefID { get; set; }
        [Display(Name = "Restaurant")]
        public int RestaurantID { get; set; }
    }
}
