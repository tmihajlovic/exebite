using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exebite.DataAccess.AutoMapper
{
    public class MealToMealEntityConverter : ITypeConverter<Meal, MealEntity>
    {
        public MealEntity Convert(Meal source, MealEntity destination, ResolutionContext context)
        {
            destination = new MealEntity();
            destination.Id = source.Id;
            destination.Price = source.Price;
            destination.Id = source.Id;
            destination.FoodEntityMealEntities = new List<FoodEntityMealEntities>();
            foreach (var food in source.Foods)
            {
                destination.FoodEntityMealEntities.Add(new FoodEntityMealEntities
                {
                    FoodEntity = AutoMapperHelper.Instance.GetMappedValue<FoodEntity>(food),
                    FoodEntityId = food.Id,
                    MealEntityId = source.Id,
                    MealEntity = destination
                });
            }

            return destination;
        }
    }
}
