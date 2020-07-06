using System;
using System.Collections.Generic;

namespace Exebite.DomainModel
{
    public class Restaurant
    {
        public long Id { get; set; }

        public string SheetId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string LogoUrl { get; set; }

        public string Description { get; set; }

        public string Contact { get; set; }

        public DateTime? OrderDue { get; set; }

        public List<Meal> Meals { get; set; } = new List<Meal>();

        public List<DailyMenu> DailyMenus { get; set; } = new List<DailyMenu>();
    }
}
