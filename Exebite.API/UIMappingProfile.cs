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
            CreateMap(typeof(CustomerModel), typeof(Customer));
            CreateMap(typeof(CreateCustomerModel), typeof(Customer));
            CreateMap(typeof(UpdateCustomerModel), typeof(Customer));
            #endregion CustomerModel
            #region CustomerAliasesModel
            CreateMap(typeof(CustomerAliases), typeof(CustomerAliasModel));
            CreateMap(typeof(CreateCustomerAliasModel), typeof(CustomerAliases));
            CreateMap(typeof(UpdateCustomerAliasModel), typeof(CustomerAliases));
            #endregion CustomerAliasesModel
            #region FoodModel
            CreateMap(typeof(Food), typeof(FoodModel));
            CreateMap(typeof(CreateFoodModel), typeof(Food));
            CreateMap(typeof(UpdateFoodModel), typeof(Food));
            #endregion  FoodModel
            #region LocationModel
            CreateMap(typeof(Location), typeof(LocationModel));
            CreateMap(typeof(UpdateLocationModel), typeof(Location));
            CreateMap(typeof(CreateLocationModel), typeof(Location));
            #endregion LocationModel
            #region MealEntity
            CreateMap(typeof(Order), typeof(OrderModel));
            CreateMap(typeof(CreateOrderModel), typeof(Order));
            CreateMap(typeof(UpdateOrderModel), typeof(Order));
            #endregion MealEntity
            #region MealModel
            CreateMap(typeof(Meal), typeof(MealModel));
            CreateMap(typeof(CreateMealModel), typeof(Meal));
            CreateMap(typeof(UpdateMealModel), typeof(Meal));
            #endregion MealModel
            #region RecipeModel
            CreateMap(typeof(Recipe), typeof(RecipeModel));
            CreateMap(typeof(CreateRecipeModel), typeof(Recipe));
            CreateMap(typeof(UpdateRecipeModel), typeof(Recipe));
            #endregion RecipeModel
            #region ResaurantModel
            CreateMap(typeof(Restaurant), typeof(RestaurantModel));
            CreateMap(typeof(RestaurantQueryDto), typeof(RestaurantQueryModel));
            #endregion  ResaurantModel
            #region DailyMenuModel
            CreateMap(typeof(DailyMenu), typeof(DailyMenuModel));
            CreateMap(typeof(UpdateDailyMenuModel), typeof(DailyMenu));
            CreateMap(typeof(CreateDailyMenuModel), typeof(DailyMenu));
            #endregion
        }
    }
}
#pragma warning restore SA1123 // Do not place regions within elements