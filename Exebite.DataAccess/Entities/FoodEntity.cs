using Exebite.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(Food))]
    public class FoodEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public FoodType Type { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public virtual List<MealEntity> Meals { get; set; }
        
        public virtual List<RecipeEntity> Recipes { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }
        public virtual RestaurantEntity Restaurant { get; set; }
    }
}
