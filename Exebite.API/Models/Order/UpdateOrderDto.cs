using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class UpdateOrderDto
    {
        [Required]
        public int[] FoodIds { get; set; }

        [Required]
        public string Note { get; set; }
    }
}