using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(Recipe))]
    public class RecipeEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }

        public virtual RestaurantEntity Restaurant { get; set; }

        [ForeignKey(nameof(MainCourse))]
        public int MainCourseId { get; set; }

        public virtual FoodEntity MainCourse { get; set; }

        public virtual List<FoodEntityRecipeEntity> FoodEntityRecipeEntities { get; set; } = new List<FoodEntityRecipeEntity>();
    }
}
