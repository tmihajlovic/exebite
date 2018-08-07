using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Exebite.DtoModels;

namespace Exebite.API.Controllers
{
    public class RecipeInsertModelDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int RestaurantId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MainCourseId { get; set; }

        [Required]
        public IEnumerable<FoodDto> SideDish { get; set; }
    }
}