using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class CreateDailyMenuDto
    {
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public List<FoodDto> Foods { get; set; }
    }
}
