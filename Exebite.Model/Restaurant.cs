using System.Collections.Generic;

namespace Exebite.Model
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Food> Foods { get; set; } = new List<Food>();

        public List<Food> DailyMenu { get; set; } = new List<Food>();

        public virtual List<Recipe> Recipes { get; set; } = new List<Recipe>();

    }
}
