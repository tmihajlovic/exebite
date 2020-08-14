using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("DailyMenu")]
    public class DailyMenuEntity
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }

        public virtual RestaurantEntity Restaurant { get; set; }

        public virtual List<DailyMenuToMealEntity> DailyMenuToMeals { get; set; } = new List<DailyMenuToMealEntity>();
    }
}
