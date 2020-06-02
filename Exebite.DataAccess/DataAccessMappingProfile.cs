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
            CreateMap<CustomerEntity, Customer>();
            CreateMap<OrderEntity, Order>();
            CreateMap<MealEntity, Food>();
            CreateMap<RestaurantEntity, Restaurant>();
            CreateMap<LocationEntity, Location>();

            // TODO: add mappings when domain models are updated.
        }

        public override string ProfileName => "DataAccessMappingProfile";
    }
}
