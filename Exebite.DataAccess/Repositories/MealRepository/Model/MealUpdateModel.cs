using Exebite.DomainModel;
using System.Collections.Generic;

namespace Exebite.DataAccess.Repositories
{
    public class MealUpdateModel
    {
        public string Name { get; set; }

        public MealType Type { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public bool IsActive { get; set; }

        public bool IsFromStandardMenu { get; set; }

        public long RestaurantId { get; set; }

        public List<Meal> Condiments { get; set; }
    }
}