using ChefsRegistry.ModelsContracts;
using ChefsRegistry.Repository;
using System.ComponentModel.DataAnnotations;

namespace ChefsRegistry.Models
{
    public class ChefModel : IChefModel
    {
        private AppDbContext @object;


        public ChefModel() { }

        public ChefModel(AppDbContext @object)
        {
            this.@object = @object;
        }

        [Display(Name = "Identifier")]
        public int ID { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string? LastName { get; set; }
        [Display(Name = "Chef's Phone Number")]
        [Required]
        public string? ChefNumber { get; set; }
    }
}
