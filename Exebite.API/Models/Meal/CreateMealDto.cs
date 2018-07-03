using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class CreateMealDto
    {
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public List<FoodDto> Foods { get; set; }
    }
}
