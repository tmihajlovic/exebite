using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.Model;

namespace Exebite.DataAccess.AutoMapper
{
    public class RecipeToRecipeEntityConverter : ITypeConverter<Recipe, RecipeEntity>
    {
        private readonly IExebiteMapper _mapper;

        public RecipeToRecipeEntityConverter(IExebiteMapper mapper)
        {
            _mapper = mapper;
        }

        public RecipeEntity Convert(Recipe source, RecipeEntity destination, ResolutionContext context)
        {
            destination = new RecipeEntity
            {
                Id = source.Id,
                MainCourse = _mapper.Map<FoodEntity>(source.MainCourse),
                MainCourseId = source.MainCourse.Id,
                Restaurant = _mapper.Map<RestaurantEntity>(source.Restaurant),
                RestaurantId = source.Restaurant.Id,
                FoodEntityRecipeEntities = new List<FoodEntityRecipeEntity>()
            };
            foreach (var food in source.SideDish)
            {
                //var fre = _dbContext.FoodEntityRecipeEntity.SingleOrDefault(i => i.FoodEntityId == food.Id && i.RecepieEntityId == source.Id);
                //if (fre == null)
                //{
                    destination.FoodEntityRecipeEntities.Add(new FoodEntityRecipeEntity
                    {
                        FoodEntity = _mapper.Map<FoodEntity>(food),
                        FoodEntityId = food.Id,
                        RecipeEntity = destination,
                        RecepieEntityId = source.Id
                    });
                //}
                //else
                //{
                //    destination.FoodEntityRecipeEntities.Add(fre);
                //}
            }

            return destination;
        }
    }
}
