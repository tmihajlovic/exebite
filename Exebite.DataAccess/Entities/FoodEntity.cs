using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Entities
{
    [Table("Food")]
    public class FoodEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public FoodType Type { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool IsInactive { get; set; }

        [ForeignKey(nameof(DailyMenuEntity))]
        public int? DailyMenuId { get; set; }

        public virtual DailyMenuEntity DailyMenu { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }

        public virtual RestaurantEntity Restaurant { get; set; }

        public virtual List<FoodEntityMealEntities> FoodEntityMealEntity { get; set; } = new List<FoodEntityMealEntities>();

        public virtual List<FoodEntityRecipeEntity> FoodEntityRecipeEntities { get; set; } = new List<FoodEntityRecipeEntity>();
    }
}
