using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("Restaurant")]
    public class RestaurantEntity
    {
        public long Id { get; set; }

        public string SheetId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string LogoUrl { get; set; }

        public string Description { get; set; }

        public string Contact { get; set; }

        public DateTime? OrderDue { get; set; }

        public virtual List<MealEntity> Meals { get; set; } = new List<MealEntity>();

        public virtual List<DailyMenuEntity> DailyMenus { get; set; } = new List<DailyMenuEntity>();
    }
}
