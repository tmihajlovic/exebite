using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class UpdateOrderModel
    {
        [Required]
        public int[] FoodIds { get; set; }

        [Required]
        public string Note { get; set; }
    }
}