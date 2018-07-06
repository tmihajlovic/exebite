using System.Linq;
using AutoMapper;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;

#pragma warning disable SA1123 // Do not place regions within elements
namespace Exebite.API
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            #region CustomerModel
            CreateMap<CustomerDto, Customer>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();
            CreateMap<CustomerQueryDto, CustomerQueryModel>();
            #endregion CustomerModel
            #region CustomerAliasesModel
            CreateMap<CustomerAliases, CustomerAliasDto>();
            CreateMap<CreateCustomerAliasDto, CustomerAliases>();
            CreateMap<UpdateCustomerAliasDto, CustomerAliases>();
            CreateMap<CustomerAliasQueryDto, CustomerAliasQueryModel>();
            #endregion CustomerAliasesModel
            #region FoodModel
            CreateMap<Food, FoodDto>();
            CreateMap<CreateFoodDto, Food>();
            CreateMap<UpdateFoodDto, Food>();
            #endregion  FoodModel
            #region LocationModel
            CreateMap<Location, LocationDto>();
            CreateMap<UpdateLocationDto, Location>();
            CreateMap<CreateLocationDto, Location>();
            CreateMap<LocationQueryDto, LocationQueryModel>();
            #endregion LocationModel
            #region Order
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<UpdateOrderDto, Order>();
            #endregion Order
            #region MealModel
            CreateMap<Meal, MealDto>()
                .ForMember(m => m.Foods, m => m.MapFrom(x => x.Foods.Select(a => a.Id)));
            CreateMap<CreateMealDto, Meal>();
            CreateMap<UpdateMealDto, Meal>();
            CreateMap<MealQueryDto, MealQueryModel>();
            #endregion MealModel
            #region RecipeModel
            CreateMap<Recipe, RecipeDto>();
            CreateMap<CreateRecipeDto, Recipe>();
            CreateMap<UpdateRecipeDto, Recipe>();
            #endregion RecipeModelgit he
            #region ResaurantModel
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantQueryDto, RestaurantQueryModel>();
            #endregion  ResaurantModel
            #region DailyMenuModel
            CreateMap<DailyMenu, DailyMenuDto>();
            CreateMap<UpdateDailyMenuDto, DailyMenu>();
            CreateMap<CreateDailyMenuDto, DailyMenu>();
            CreateMap<DailyMenuQueryDto, DailyMenuQueryModel>();
            #endregion
        }
    }
}
#pragma warning restore SA1123 // Do not place regions within elements