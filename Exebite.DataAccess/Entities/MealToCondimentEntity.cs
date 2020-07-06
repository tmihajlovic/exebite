using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("MealToCondiment")]
    public class MealToCondimentEntity
    {
        [ForeignKey(nameof(Meal))]
        public long MealId { get; set; }

        public virtual MealEntity Meal { get; set; }

        [ForeignKey(nameof(Condiment))]
        public long CondimentId { get; set; }

        public virtual MealEntity Condiment { get; set; }
    }
}
