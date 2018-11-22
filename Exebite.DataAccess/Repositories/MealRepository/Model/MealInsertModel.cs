using System.Collections.Generic;

namespace Exebite.DataAccess.Repositories
{
    public class MealInsertModel
    {
        public decimal Price { get; set; }

        public virtual List<int> Foods { get; set; } = new List<int>();
    }
}