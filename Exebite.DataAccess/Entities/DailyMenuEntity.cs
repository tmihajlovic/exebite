using System;
using System.ComponentModel.DataAnnotations;
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
    }
}
