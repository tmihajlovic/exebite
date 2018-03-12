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
    public class MealToMealEntityConverter : ITypeConverter<Meal, MealEntity>
    {
        private FoodOrderingContext _dbContext;

        public MealEntity Convert(Meal source, MealEntity destination, ResolutionContext context)
        {
            _dbContext = (FoodOrderingContext)context.Options.Items["dbContext"];
            destination = new MealEntity();
            destination.Id = source.Id;
            destination.Price = source.Price;
            destination.Id = source.Id;
            destination.FoodEntityMealEntities = new List<FoodEntityMealEntities>();
            foreach (var food in source.Foods)
            {
                var dbFoodMealEntity = _dbContext.FoodEntityMealEntities.SingleOrDefault(fm => fm.FoodEntityId == food.Id && fm.MealEntityId == source.Id);
                if (dbFoodMealEntity == null)
                {
                    destination.FoodEntityMealEntities.Add(new FoodEntityMealEntities
                    {
                        FoodEntity = AutoMapperHelper.Instance.GetMappedValue<FoodEntity>(food, _dbContext),
                        FoodEntityId = food.Id,
                        MealEntityId = source.Id,
                        MealEntity = destination
                    });
                }
                else
                {
                    destination.FoodEntityMealEntities.Add(dbFoodMealEntity);
                }
            }

            return destination;
        }
    }
}
