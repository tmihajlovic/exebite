using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("DailyMenuToMeal")]
    public class DailyMenuToMealEntity
    {
        [ForeignKey(nameof(Meal))]
        public long MealId { get; set; }

        public virtual MealEntity Meal { get; set; }

        [ForeignKey(nameof(DailyMenu))]
        public long DailyMenuId { get; set; }

        public virtual DailyMenuEntity DailyMenu { get; set; }
    }
}
