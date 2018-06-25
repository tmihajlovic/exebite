using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class CreateCustomerAliasModel
    {
        [Required]
        public string Alias { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int RestaurantId { get; set; }
    }
}
