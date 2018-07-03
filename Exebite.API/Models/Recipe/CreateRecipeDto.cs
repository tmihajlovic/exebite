using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class CreateRecipeDto
    {
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public int FoodId { get; set; }

        [Required]
        public List<FoodDto> SideDish { get; set; }
    }
}
