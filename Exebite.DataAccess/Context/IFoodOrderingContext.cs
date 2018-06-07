using Exebite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Migrations
{
    public interface IFoodOrderingContext
    {
        DbSet<CustomerAliasesEntities> CustomerAliases { get; set; }

        DbSet<CustomerEntity> Customers { get; set; }

        DbSet<FoodEntityMealEntities> FoodEntityMealEntities { get; set; }

        DbSet<FoodEntityRecipeEntity> FoodEntityRecipeEntity { get; set; }

        DbSet<FoodEntity> Foods { get; set; }

        DbSet<LocationEntity> Locations { get; set; }

        DbSet<MealEntity> Meals { get; set; }

        DbSet<OrderEntity> Orders { get; set; }

        DbSet<RecipeEntity> Recipes { get; set; }

        DbSet<RestaurantEntity> Restaurants { get; set; }
    }
}