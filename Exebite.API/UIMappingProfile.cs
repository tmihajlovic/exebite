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
            CreateMap<Location, LocationDto>();
            CreateMap<LocationQueryDto, LocationQueryModel>();
            CreateMap<Meal, MealDto>();
            CreateMap<MealQueryDto, MealQueryModel>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderQueryDto, OrderQueryModel>();
            CreateMap<OrderToMeal, OrderToMealDto>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentQueryDto, PaymentQueryModel>();
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantQueryDto, RestaurantQueryModel>();
        }
    }
}