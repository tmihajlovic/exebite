using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("Meal")]
    public class MealEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public virtual RestaurantEntity Restaurant { get; set; }

        public virtual List<MealToCondimentEntity> Condiments { get; set; } = new List<MealToCondimentEntity>();

        public virtual List<OrderToMealEntity> OrdersToMeals { get; set; } = new List<OrderToMealEntity>();
    }
}
