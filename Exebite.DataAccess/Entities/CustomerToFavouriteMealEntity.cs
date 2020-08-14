﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("CustomerToFavouriteMeal")]
    public class CustomerToFavouriteMealEntity
    {
        [ForeignKey(nameof(Meal))]
        public long MealId { get; set; }

        public virtual MealEntity Meal { get; set; }

        [ForeignKey(nameof(Customer))]
        public long CustomerId { get; set; }

        public virtual CustomerEntity Customer { get; set; }
    }
}
