using System.Collections.Generic;

namespace Exebite.DtoModels
{
    public class DailyMenuDto
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public List<FoodDto> Foods { get; set; }
    }
}
