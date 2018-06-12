using AutoMapper;
using Exebite.API.Models;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.Model;

namespace Exebite.API
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            CreateMap(typeof(Restaurant), typeof(RestaurantViewModel));
            CreateMap(typeof(Location), typeof(LocationModel));
            CreateMap(typeof(UpdateLocationModel), typeof(Location));
            CreateMap(typeof(CreateLocationModel), typeof(Location));
            CreateMap(typeof(Food), typeof(FoodModel));
            CreateMap(typeof(CreateFoodModel), typeof(Food));
            CreateMap(typeof(UpdateFoodModel), typeof(Food));
            CreateMap(typeof(Food), typeof(FoodEntity)).ConvertUsing<IFoodToFoodEntityConverter>();
            CreateMap(typeof(Recipe), typeof(RecipeEntity)).ConvertUsing<IRecipeToRecipeEntityConverter>();
            CreateMap(typeof(Meal), typeof(MealEntity)).ConvertUsing<IMealToMealEntityConverter>();
            CreateMap(typeof(Order), typeof(OrderModel));
            CreateMap(typeof(CreateOrderModel), typeof(Order));

        }
    }
}
