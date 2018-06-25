using System.Collections.Generic;

namespace Exebite.DomainModel
{
    public class Recipe
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public int MainCourseId { get; set; }

        public Food MainCourse { get; set; }

        public List<Food> SideDish { get; set; } = new List<Food>();
    }
}
