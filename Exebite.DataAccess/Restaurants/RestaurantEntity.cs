using Exebite.DataAccess.Foods;
using Exebite.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Restaurants
{
    [Table(nameof(Restaurant))]
    public class RestaurantEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<FoodEntity> Foods { get; set; }

        public virtual List<FoodEntity> DailyMenu { get; set; }

        public virtual List<FoodEntity> AlwaysAvailable { get; set; }
    }
}
