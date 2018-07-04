using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public class MealInsertModel
    {
        public decimal Price { get; set; }

        public virtual List<Food> Foods { get; set; } = new List<Food>();
    }
}