using System;
using System.Collections.Generic;

namespace Exebite.DtoModels
{
    public class DailyMenuDto
    {
        public int Id { get; set; }

        public long RestaurantId { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public List<MealDto> Meals { get; set; }
    }
}
