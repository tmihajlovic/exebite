using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;

namespace Exebite.DataAccess.AutoMapper
{
    [Obsolete]
    public class MealToMealEntityConverter : IMealToMealEntityConverter
    {
        //private readonly IFoodOrderingContextFactory _factory;

        //public MealToMealEntityConverter(IFoodOrderingContextFactory factory)
        //{
        //    _factory = factory;
        //}

        //public MealEntity Convert(Food source, MealEntity destination, ResolutionContext context)
        //{
        //    using (var dbContext = _factory.Create())
        //    {
        //        destination = new MealEntity
        //        {
        //            Id = source.Id,
        //            Name = source.Name,
        //            Type = source.Type,
        //            Price = source.Price,
        //            Description = source.Description,
        //            IsInactive = source.IsInactive,
        //            RestaurantId = source.RestaurantId,
        //            FoodEntityMealEntities = new List<FoodEntityMealEntity>(),
        //            FoodEntityRecipeEntities = new List<FoodEntityRecipeEntity>()
        //        };
        //        destination.Restaurant = dbContext.Restaurant.Find(destination.RestaurantId);
        //        destination.FoodEntityMealEntities = dbContext.Food.Where(fme => fme.Id == source.Id).SelectMany(x => x.FoodEntityMealEntities).ToList();
        //        destination.FoodEntityRecipeEntities = dbContext.Recipe.Where(fre => fre.Id == source.Id).SelectMany(x => x.FoodEntityRecipeEntities).ToList();

        //        return destination;
        //    }
        //}
    }
}
