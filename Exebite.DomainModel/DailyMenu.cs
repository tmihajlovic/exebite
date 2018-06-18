using System.Collections.Generic;

namespace Exebite.DomainModel
{
    public class DailyMenu
    {
        public int Id { get; set; }

        public List<Food> Foods { get; set; } = new List<Food>();

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
