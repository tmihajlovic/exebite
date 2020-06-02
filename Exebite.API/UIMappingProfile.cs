using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.DtoModels;

#pragma warning disable SA1123 // Do not place regions within elements
namespace Exebite.API
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            #region CustomerModel
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerQueryDto, CustomerQueryModel>();
            #endregion CustomerModel
            #region CustomerAliasesModel
            CreateMap<CustomerAliases, CustomerAliasDto>();
            #endregion CustomerAliasesModel
            #region FoodModel
            CreateMap<Food, FoodDto>();
            CreateMap<FoodQueryModelDto, MealQueryModel>();
            #endregion  FoodModel
            #region LocationModel
            CreateMap<Location, LocationDto>();
            CreateMap<LocationQueryDto, LocationQueryModel>();
            #endregion LocationModel
            #region Order
            CreateMap<Order, OrderDto>();
            CreateMap<OrderQueryDto, OrderQueryModel>();
            #endregion Order
            #region MealModel
            CreateMap<Meal, MealDto>();
            CreateMap<MealQueryDto, MealQueryModel>();
            #endregion MealModel
            #region RecipeModel
            CreateMap<Recipe, RecipeDto>();
            #endregion RecipeModel
            #region RestaurantModel
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantQueryDto, RestaurantQueryModel>();
            #endregion  RestaurantModel
            #region DailyMenuModel
            CreateMap<DailyMenu, DailyMenuDto>();
            CreateMap<DailyMenuQueryDto, DailyMenuQueryModel>();
            #endregion
            #region Role
            CreateMap<Role, RoleDto>();
            #endregion Role
        }
    }
}
#pragma warning restore SA1123 // Do not place regions within elements