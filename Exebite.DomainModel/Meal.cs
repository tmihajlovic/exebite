using System.Collections.Generic;

namespace Exebite.DomainModel
{
    public class Meal
    {
        public int Id { get; set; }

        public List<Food> Foods { get; set; } = new List<Food>();

        public decimal Price { get; set; }
    }
}
