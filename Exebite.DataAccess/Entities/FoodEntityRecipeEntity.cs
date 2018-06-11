using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(FoodEntityRecipeEntity))]
    public class FoodEntityRecipeEntity
    {
        public int FoodEntityId { get; set; }

        [ForeignKey("FoodEntityId")]
        public virtual FoodEntity FoodEntity { get; set; }

        public int RecepieEntityId { get; set; }

        [ForeignKey("RecepieEntityId")]
        public virtual RecipeEntity RecipeEntity { get; set; }
    }
}
