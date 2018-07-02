using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;

namespace Exebite.DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<CustomerEntity, Customer>();
            CreateMap<CustomerAliasesEntities, CustomerAliases>();
            CreateMap<OrderEntity, Order>();
            CreateMap<FoodEntity, Food>();
            CreateMap<RestaurantEntity, Restaurant>();
            CreateMap<RestaurantInsertModel, Restaurant>();
            CreateMap<RestaurantUpdateModel, Restaurant>();
            CreateMap<LocationEntity, Location>();
            CreateMap<RecipeEntity, Recipe>()
                .ForMember(r => r.SideDish, v => v.MapFrom(c => c.FoodEntityRecipeEntities.Select(re => re.FoodEntity).ToList()));
            CreateMap<DailyMenuEntity, DailyMenu>();
            CreateMap<MealEntity, Meal>()
                .ForMember(f => f.Foods, v => v.MapFrom(c => c.FoodEntityMealEntities.Select(fl => fl.FoodEntity).ToList()));
        }

        public override string ProfileName => "DataAccessMappingProfile";
    }
}
