using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.AutoMapper
{
    public class RecipeToRecipeEntityConverter : ITypeConverter<Recipe, RecipeEntity>
    {
        private FoodOrderingContext _dbContext;

        public RecipeEntity Convert(Recipe source, RecipeEntity destination, ResolutionContext context)
        {
            _dbContext = (FoodOrderingContext)context.Options.Items["dbContext"];
            destination = new RecipeEntity
            {
                Id = source.Id,
                MainCourse = AutoMapperHelper.Instance.GetMappedValue<FoodEntity>(source.MainCourse, _dbContext),
                MainCourseId = source.MainCourse.Id,
                Restaurant = AutoMapperHelper.Instance.GetMappedValue<RestaurantEntity>(source.Restaurant, _dbContext),
                RestaurantId = source.Restaurant.Id,
                FoodEntityRecipeEntities = new List<FoodEntityRecipeEntity>()
            };
            foreach (var food in source.SideDish)
            {
                var fre = _dbContext.FoodEntityRecipeEntity.SingleOrDefault(i => i.FoodEntityId == food.Id && i.RecepieEntityId == source.Id);
                if (fre == null)
                {
                    destination.FoodEntityRecipeEntities.Add(new FoodEntityRecipeEntity
                    {
                        FoodEntity = AutoMapperHelper.Instance.GetMappedValue<FoodEntity>(food, _dbContext),
                        FoodEntityId = food.Id,
                        RecipeEntity = destination,
                        RecepieEntityId = source.Id
                    });
                }
                else
                {
                    destination.FoodEntityRecipeEntities.Add(fre);
                }
            }

            return destination;
        }
    }
}
