using Exebite.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(Recipe))]
    public class RecipeEntity
    {
        [Key]
        public int Id { get; set; }
    
        [ForeignKey(nameof(MainCourse))]
        public int MainCourseId { get; set; }
        public virtual FoodEntity MainCourse { get; set; }

        public virtual List<FoodEntity> Foods { get; set; }
    }
}
