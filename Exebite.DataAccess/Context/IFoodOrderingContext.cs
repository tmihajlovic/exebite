using Exebite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Context
{
    public interface IFoodOrderingContext
    {
        DbSet<CustomerAliasesEntities> CustomerAlias { get; set; }

        DbSet<CustomerEntity> Customer { get; set; }

        DbSet<FoodEntity> Food { get; set; }

        DbSet<LocationEntity> Location { get; set; }

        DbSet<MealEntity> Meal { get; set; }

        DbSet<OrderEntity> Order { get; set; }

        DbSet<RecipeEntity> Recipe { get; set; }

        DbSet<RestaurantEntity> Restaurant { get; set; }
    }
}