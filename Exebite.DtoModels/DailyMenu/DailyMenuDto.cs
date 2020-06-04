using System;
using System.Collections.Generic;

namespace Exebite.DtoModels
{
    public class DailyMenuDto
    {
        public int Id { get; set; }

        public RestaurantDto Restaurant { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public List<MealDto> Foods { get; set; }
    }
}
