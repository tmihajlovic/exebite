using System.ComponentModel.DataAnnotations;

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
    }
}