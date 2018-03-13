using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exebite.Model;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(Restaurant))]
    public class RestaurantEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<FoodEntity> Foods { get; set; }

        public virtual List<FoodEntity> DailyMenu { get; set; }
    }
}
