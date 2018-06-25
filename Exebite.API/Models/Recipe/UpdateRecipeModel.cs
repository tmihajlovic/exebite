using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class UpdateRecipeModel
    {
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public int FoodId { get; set; }
    }
}
