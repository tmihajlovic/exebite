using AutoMapper;
using Exebite.Business.Model;
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
            CreateMap<CreateOrderDto, RestaurantOrder>();
            CreateMap<RestaurantOrder, OrderInsertModel>();
            CreateMap<UpdateOrderDto, RestaurantOrder>();
            CreateMap<RestaurantOrder, OrderUpdateModel>();
            CreateMap<CreateOrderToMealDto, Meal>().ForMember(d => d.Id, opt => opt.MapFrom(src => src.MealId));
            CreateMap<OrderToMeal, OrderToMealDto>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentQueryDto, PaymentQueryModel>();
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantQueryDto, RestaurantQueryModel>();
        }
    }
}