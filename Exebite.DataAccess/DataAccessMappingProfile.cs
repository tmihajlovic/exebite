using System.Linq;
using AutoMapper;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.Model;

namespace Exebite.DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap(typeof(CustomerEntity), typeof(Customer));
            CreateMap(typeof(OrderEntity), typeof(Order));
            CreateMap(typeof(FoodEntity), typeof(Food));
            CreateMap(typeof(RestaurantEntity), typeof(Restaurant));
            CreateMap(typeof(LocationEntity), typeof(Location));
            CreateMap(typeof(RecipeEntity), typeof(Recipe));
            CreateMap<RecipeEntity, Recipe>().ForMember(r => r.SideDish, v => v.MapFrom(c => c.FoodEntityRecipeEntities.Select(re => re.FoodEntity).ToList()));
            CreateMap(typeof(MealEntity), typeof(Meal));
            CreateMap<MealEntity, Meal>().ForMember(f => f.Foods, v => v.MapFrom(c => c.FoodEntityMealEntities.Select(fl => fl.FoodEntity).ToList())); // Populate Food list from helper property for many-to-many
            CreateMap(typeof(CustomerAliasesEntities), typeof(CustomerAliases));
            CreateMap(typeof(Customer), typeof(CustomerEntity));
            CreateMap<Customer, CustomerEntity>().ForMember(i => i.LocationId, option => option.MapFrom(c => c.Location.Id));
            CreateMap(typeof(Order), typeof(OrderEntity));
            CreateMap(typeof(Food), typeof(FoodEntity));//.ConvertUsing<IFoodToFoodEntityConverter>();
            CreateMap(typeof(Restaurant), typeof(RestaurantEntity));
            CreateMap(typeof(Location), typeof(LocationEntity));
            CreateMap(typeof(Recipe), typeof(RecipeEntity)).ConvertUsing<IRecipeToRecipeEntityConverter>();
            CreateMap(typeof(Meal), typeof(MealEntity)).ConvertUsing<IMealToMealEntityConverter>();
            CreateMap(typeof(CustomerAliases), typeof(CustomerAliasesEntities));
        }

        public override string ProfileName => "DataAccessMappingProfile";
    }
}
