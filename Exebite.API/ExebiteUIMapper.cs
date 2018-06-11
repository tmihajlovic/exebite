using System;
using AutoMapper;
using Exebite.API.Models;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.Model;

namespace Exebite.API
{
    public class ExebiteUIMapper : Mapper, IExebiteMapper
    {
        public ExebiteUIMapper(IServiceProvider provider)
          : base(new MapperConfiguration(cfg =>
          {
              cfg.CreateMap(typeof(Restaurant), typeof(RestaurantViewModel));
              cfg.CreateMap(typeof(Location), typeof(LocationViewModel));
              cfg.CreateMap(typeof(UpdateLocationModel), typeof(Location));
              cfg.CreateMap(typeof(CreateLocationModel), typeof(Location));
              cfg.CreateMap(typeof(Food), typeof(FoodViewModel));
              cfg.CreateMap(typeof(CreateFoodModel), typeof(Food));
              cfg.CreateMap(typeof(UpdateFoodModel), typeof(Food));


              cfg.CreateMap(typeof(Food), typeof(FoodEntity)).ConvertUsing<FoodToFoodEntityConverter>();
              cfg.CreateMap(typeof(Recipe), typeof(RecipeEntity)).ConvertUsing<RecipeToRecipeEntityConverter>();
              cfg.CreateMap(typeof(Meal), typeof(MealEntity)).ConvertUsing<MealToMealEntityConverter>();
          }))
        {
        }
    }
}
