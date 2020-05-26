using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("OrderToMeal")]
    public class OrderToMealEntity
    {
        public long Id { get; set; }

        public int Quantity { get; set; }

        public string Note { get; set; }

        [ForeignKey(nameof(Order))]
        public long OrderId { get; set; }

        public virtual OrderEntity Order { get; set; }

        [ForeignKey(nameof(Meal))]
        public long MealId { get; set; }

        public virtual MealEntity Meal { get; set; }
    }
}
