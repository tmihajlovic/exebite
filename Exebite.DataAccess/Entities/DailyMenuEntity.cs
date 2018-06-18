using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(DailyMenuEntity))]
    public class DailyMenuEntity
    {
        public int Id { get; set; }

        public virtual List<FoodEntity> Menu { get; set; } = new List<FoodEntity>();

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }

        public virtual RestaurantEntity Restaurant { get; set; }
    }
}
