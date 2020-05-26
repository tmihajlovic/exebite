using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("Customer")]
    public class CustomerEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string GoogleUserId { get; set; }

        public int Role { get; set; }

        [ForeignKey(nameof(DefaultLocation))]
        public long DefaultLocationId { get; set; }

        public virtual LocationEntity DefaultLocation { get; set; }

        public virtual List<OrderEntity> Orders { get; set; }

        public virtual List<MealEntity> FavouriteMeals { get; set; }
    }
}
