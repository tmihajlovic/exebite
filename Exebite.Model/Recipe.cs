using System.Collections.Generic;

namespace Exebite.Model
{
    public class Recipe
    {
        public int Id { get; set; }

        // todo: not sure that this should be here 
        public Restaurant Restaurant { get; set; }

        public Food MainCourse { get; set; }

        public List<Food> SideDish { get; set; } = new List<Food>();
    }
}
