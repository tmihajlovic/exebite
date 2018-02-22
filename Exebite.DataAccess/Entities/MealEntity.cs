using Exebite.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(Meal))]
    public class MealEntity
    {
        [Key]
        public int Id { get; set; }
        
        public decimal Price { get; set; }

        public virtual List<FoodEntity> Foods { get; set; }
        
        public virtual List<OrderEntity> Orders { get; set; }
    }
}
