using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exebite.Model;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(Meal))]
    public class MealEntity
    {
        [Key]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public virtual List<FoodEntityMealEntities> FoodEntityMealEntities { get; set; } = new List<FoodEntityMealEntities>();
    }
}
