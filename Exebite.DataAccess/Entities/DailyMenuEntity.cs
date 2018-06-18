using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(DailyMenuEntity))]
    public class DailyMenuEntity
    {
        public int FoodEntityId { get; set; }

        [ForeignKey("FoodEntityId")]
        public virtual FoodEntity FoodEntity { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }

        public virtual RestaurantEntity Restaurant { get; set; }
    }
}
