using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("Restaurant")]
    public class RestaurantEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<FoodEntity> Foods { get; set; } = new List<FoodEntity>();

        public virtual List<RecipeEntity> Recipes { get; set; } = new List<RecipeEntity>();
    }
}
