using System.Collections.Generic;

namespace Exebite.Model
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Food> Foods { get; set; }

        public List<Food> DailyMenu { get; set; }
    }
}
