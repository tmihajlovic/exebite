using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exebite.DataAccess.AutoMapper
{
    public class FoodToFoodEntityConverter : ITypeConverter<Food, FoodEntity>
    {
        private FoodOrderingContext _dbContext;

        public FoodEntity Convert(Food source, FoodEntity destination, ResolutionContext context)
        {
            _dbContext = (FoodOrderingContext)context.Options.Items["dbContext"];

            destination = new FoodEntity
            {
                Id = source.Id,
                Name = source.Name,
                Type = source.Type,
                Price = source.Price,
                Description = source.Description,
                IsInactive = source.IsInactive,
                RestaurantId = source.Restaurant.Id,
                FoodEntityMealEntity = new List<FoodEntityMealEntities>(),
                FoodEntityRecipeEntities = new List<FoodEntityRecipeEntity>()
            };
                destination.Restaurant = _dbContext.Restaurants.Find(destination.RestaurantId);
                destination.FoodEntityMealEntity = _dbContext.FoodEntityMealEntities.Where(fme => fme.FoodEntityId == source.Id).ToList();
                destination.FoodEntityRecipeEntities = _dbContext.FoodEntityRecipeEntity.Where(fre => fre.FoodEntityId == source.Id).ToList();

            return destination;
        }
    }
}
