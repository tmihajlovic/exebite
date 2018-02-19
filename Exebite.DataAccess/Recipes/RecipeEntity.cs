using Exebite.DataAccess.Foods;
using Exebite.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Recipes
{
    [Table(nameof(Recipe))]
    public class RecipeEntity
    {
        [Key]
        public int Id { get; set; }

        public virtual List<FoodEntity> Foods { get; set; }
    }
}
