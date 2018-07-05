using System.Collections.Generic;

namespace Exebite.API.Models
{
    public class MealDto
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public List<int> Foods { get; set; }
    }
}
