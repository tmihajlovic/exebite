using System;
using System.Collections.Generic;

namespace Exebite.DtoModels
{
    public class RestaurantDto
    {
        public long Id { get; set; }

        public string SheetId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string LogoUrl { get; set; }

        public string Description { get; set; }

        public string Contact { get; set; }

        public bool IsActive { get; set; }

        public DateTime? OrderDue { get; set; }

        public List<MealDto> Meals { get; set; }

        public List<DailyMenuDto> DailyMenus { get; set; }
    }
}
