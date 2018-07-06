using System.Collections.Generic;

namespace Exebite.DataAccess.Repositories
{
    public class MealUpdateModel
    {
        public decimal Price { get; set; }

        public virtual List<int> Foods { get; set; } = new List<int>();
    }
}