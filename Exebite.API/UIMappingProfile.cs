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
            CreateMap<MealQueryDto, MealQueryModel>();
            CreateMap<Location, LocationDto>();
            CreateMap<LocationQueryDto, LocationQueryModel>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderQueryDto, OrderQueryModel>();
            CreateMap<Meal, MealDto>();
            CreateMap<MealQueryDto, MealQueryModel>();
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantQueryDto, RestaurantQueryModel>();
            CreateMap<DailyMenu, DailyMenuDto>();
            CreateMap<DailyMenuQueryDto, DailyMenuQueryModel>();
        }
    }
}