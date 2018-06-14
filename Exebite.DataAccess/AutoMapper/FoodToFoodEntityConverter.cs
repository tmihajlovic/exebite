using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.Model;

namespace Exebite.DataAccess.AutoMapper
{
    public class FoodToFoodEntityConverter : IFoodToFoodEntityConverter
    {
        private readonly IFoodOrderingContextFactory _factory;

        public FoodToFoodEntityConverter(IFoodOrderingContextFactory factory)
        {
            _factory = factory;
        }

        public FoodEntity Convert(Food source, FoodEntity destination, ResolutionContext context)
        {
            using (var dbContext = _factory.Create())
            {
                destination = new FoodEntity
                {
                    Id = source.Id,
                    Name = source.Name,
                    Type = source.Type,
                    Price = source.Price,
                    Description = source.Description,
                    IsInactive = source.IsInactive,
                    RestaurantId = source.RestaurantId,
                    FoodEntityMealEntity = new List<FoodEntityMealEntities>(),
                    FoodEntityRecipeEntities = new List<FoodEntityRecipeEntity>()
                };
                destination.Restaurant = dbContext.Restaurants.Find(destination.RestaurantId);
                destination.FoodEntityMealEntity = dbContext.FoodEntityMealEntities.Where(fme => fme.FoodEntityId == source.Id).ToList();
                destination.FoodEntityRecipeEntities = dbContext.FoodEntityRecipeEntity.Where(fre => fre.FoodEntityId == source.Id).ToList();

                return destination;
            }
        }
    }
}
