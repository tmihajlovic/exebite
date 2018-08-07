using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class UpdateRecipeDto
    {
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public int MainCourseId { get; set; }

        [Required]
        public List<FoodDto> SideDish { get; set; }
    }
}
