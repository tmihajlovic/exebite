using AutoMapper;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.DtoModels;

namespace Exebite.API
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerQueryDto, CustomerQueryModel>();
            CreateMap<CustomerAliases, CustomerAliasDto>();
            CreateMap<Food, MealDto>();
            CreateMap<MealQueryDto, MealQueryModel>();
            CreateMap<Location, LocationDto>();
            CreateMap<LocationQueryDto, LocationQueryModel>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderQueryDto, OrderQueryModel>();
            CreateMap<Meal, MealDto>();
            CreateMap<MealQueryDto, MealQueryModel>();
            CreateMap<Recipe, RecipeDto>();
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantQueryDto, RestaurantQueryModel>();
            CreateMap<DailyMenu, DailyMenuDto>();
            CreateMap<DailyMenuQueryDto, DailyMenuQueryModel>();
            CreateMap<Role, RoleDto>();
        }
    }
}