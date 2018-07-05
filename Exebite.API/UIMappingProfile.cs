using AutoMapper;
using Exebite.API.Models;
using Exebite.DomainModel;

#pragma warning disable SA1123 // Do not place regions within elements
namespace Exebite.API
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            #region CustomerModel
            CreateMap(typeof(CustomerDto), typeof(Customer));
            CreateMap(typeof(CreateCustomerDto), typeof(Customer));
            CreateMap(typeof(UpdateCustomerDto), typeof(Customer));
            CreateMap(typeof(CustomerQueryDto), typeof(CustomerQueryModel));
            #endregion CustomerModel
            #region CustomerAliasesModel
            CreateMap(typeof(CustomerAliases), typeof(CustomerAliasDto));
            CreateMap(typeof(CreateCustomerAliasDto), typeof(CustomerAliases));
            CreateMap(typeof(UpdateCustomerAliasDto), typeof(CustomerAliases));
            CreateMap(typeof(CustomerAliasQueryDto), typeof(CustomerAliasQueryModel));
            #endregion CustomerAliasesModel
            #region FoodModel
            CreateMap(typeof(Food), typeof(FoodDto));
            CreateMap(typeof(CreateFoodDto), typeof(Food));
            CreateMap(typeof(UpdateFoodDto), typeof(Food));
            #endregion  FoodModel
            #region LocationModel
            CreateMap(typeof(Location), typeof(LocationDto));
            CreateMap(typeof(UpdateLocationDto), typeof(Location));
            CreateMap(typeof(CreateLocationDto), typeof(Location));
            CreateMap(typeof(LocationQueryDto), typeof(LocationQueryModel));
            #endregion LocationModel
            #region MealEntity
            CreateMap(typeof(Order), typeof(OrderDto));
            CreateMap(typeof(CreateOrderDto), typeof(Order));
            CreateMap(typeof(UpdateOrderDto), typeof(Order));
            CreateMap(typeof(MealQueryDto), typeof(MealQueryModel));
            #endregion MealEntity
            #region MealModel
            CreateMap(typeof(Meal), typeof(MealDto));
            CreateMap(typeof(CreateMealDto), typeof(Meal));
            CreateMap(typeof(UpdateMealDto), typeof(Meal));
            #endregion MealModel
            #region RecipeModel
            CreateMap(typeof(Recipe), typeof(RecipeDto));
            CreateMap(typeof(CreateRecipeDto), typeof(Recipe));
            CreateMap(typeof(UpdateRecipeDto), typeof(Recipe));
            #endregion RecipeModel
            #region ResaurantModel
            CreateMap(typeof(Restaurant), typeof(RestaurantDto));
            CreateMap(typeof(RestaurantQueryDto), typeof(RestaurantQueryModel));
            #endregion  ResaurantModel
            #region DailyMenuModel
            CreateMap(typeof(DailyMenu), typeof(DailyMenuDto));
            CreateMap(typeof(UpdateDailyMenuDto), typeof(DailyMenu));
            CreateMap(typeof(CreateDailyMenuDto), typeof(DailyMenu));
            CreateMap(typeof(DailyMenuQueryDto), typeof(DailyMenuQueryModel));
            #endregion
        }
    }
}
#pragma warning restore SA1123 // Do not place regions within elements