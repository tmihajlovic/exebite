using Exebite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Context
{
    public interface IMealOrderingContext
    {
        DbSet<CustomerEntity> Customer { get; set; }

        DbSet<LocationEntity> Location { get; set; }

        DbSet<MealEntity> Meal { get; set; }

        DbSet<OrderEntity> Order { get; set; }

        DbSet<RestaurantEntity> Restaurant { get; set; }
    }
}