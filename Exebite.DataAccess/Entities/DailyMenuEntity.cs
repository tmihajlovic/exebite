using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("DailyMenu")]
    public class DailyMenuEntity
    {
        [Key]
        public int Id { get; set; }

        public virtual List<FoodEntity> Foods { get; set; } = new List<FoodEntity>();

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }

        public virtual RestaurantEntity Restaurant { get; set; }
    }
}
