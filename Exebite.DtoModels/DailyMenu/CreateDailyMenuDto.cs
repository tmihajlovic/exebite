using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class CreateDailyMenuDto
    {
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public List<FoodDto> Foods { get; set; }
    }
}
