using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;

namespace Exebite.DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<CustomerEntity, Customer>()
                .ForMember(c => c.FavouriteMeals, e => e.MapFrom(p => p.FavouriteMeals.Select(m => m.Meal)));
            CreateMap<DailyMenuEntity, DailyMenu>()
                .ForMember(dm => dm.Meals, e => e.MapFrom(p => p.DailyMenuToMeals.Select(dm => dm.Meal)));
            CreateMap<LocationEntity, Location>();
            CreateMap<MealEntity, Meal>()
                .ForMember(m => m.Condiments, e => e.MapFrom(p => p.Condiments.Select(c => c.Condiment)));
            CreateMap<OrderEntity, Order>();
            CreateMap<RestaurantEntity, Restaurant>();
            CreateMap<PaymentEntity, Payment>();
        }

        public override string ProfileName => "DataAccessMappingProfile";
    }
}
