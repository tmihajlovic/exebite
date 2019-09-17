using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("Meal")]
    public class MealEntity
    {
        [Key]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public virtual List<FoodEntityMealEntity> FoodEntityMealEntities { get; set; } = new List<FoodEntityMealEntity>();
    }
}
