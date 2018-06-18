using System.Collections.Generic;

namespace Exebite.Model
{
    public class Recipe
    {
        public int Id { get; set; }

        public Restaurant Restaurant { get; set; }

        public Food MainCourse { get; set; }

        public List<Food> SideDish { get; set; } = new List<Food>();
    }
}
