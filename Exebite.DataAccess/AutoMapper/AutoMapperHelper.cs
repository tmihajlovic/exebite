using System.Linq;
using AutoMapper;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess
{
    /// <summary>
    /// Singleton class. Helps with using automapper
    /// </summary>
    public class AutoMapperHelper
    {
        private static object _locker = new object();
        private static AutoMapperHelper _instance = null;

        private AutoMapperHelper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap(typeof(CustomerEntity), typeof(Customer));
                cfg.CreateMap(typeof(OrderEntity), typeof(Order));
                cfg.CreateMap(typeof(FoodEntity), typeof(Food));
                cfg.CreateMap(typeof(RestaurantEntity), typeof(Restaurant));
                cfg.CreateMap(typeof(LocationEntity), typeof(Location));
                cfg.CreateMap(typeof(RecipeEntity), typeof(Recipe));
                cfg.CreateMap<RecipeEntity, Recipe>().ForMember(r => r.SideDish, v => v.MapFrom(c => c.FoodEntityRecipeEntities.Select(re => re.FoodEntity).ToList()));
                cfg.CreateMap(typeof(MealEntity), typeof(Meal));
                cfg.CreateMap<MealEntity, Meal>().ForMember(f => f.Foods, v => v.MapFrom(c => c.FoodEntityMealEntities.Select(fl => fl.FoodEntity).ToList())); // Populate Food list from helper property for many-to-many
                cfg.CreateMap(typeof(CustomerAliasesEntities), typeof(CustomerAliases));

                cfg.CreateMap(typeof(Customer), typeof(CustomerEntity));
                cfg.CreateMap<Customer, CustomerEntity>().ForMember(i => i.LocationId, option => option.MapFrom(c => c.Location.Id));
                cfg.CreateMap(typeof(Order), typeof(OrderEntity));
                cfg.CreateMap(typeof(Food), typeof(FoodEntity)).ConvertUsing<FoodToFoodEntityConverter>();
                cfg.CreateMap(typeof(Restaurant), typeof(RestaurantEntity));
                cfg.CreateMap(typeof(Location), typeof(LocationEntity));
                cfg.CreateMap(typeof(Recipe), typeof(RecipeEntity)).ConvertUsing<RecipeToRecipeEntityConverter>();
                cfg.CreateMap(typeof(Meal), typeof(MealEntity)).ConvertUsing<MealToMealEntityConverter>();
                cfg.CreateMap(typeof(CustomerAliases), typeof(CustomerAliasesEntities));
            });
        }

        /// <summary>
        /// Gets instance of the class
        /// </summary>
        public static AutoMapperHelper Instance
        {
            get
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new AutoMapperHelper();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Maps one object to another. Used for entity mapping.
        /// </summary>
        /// <typeparam name="T">Destination object type</typeparam>
        /// <param name="value">Source object value</param>
        /// <param name="dbContext">Database context</param>
        /// <returns>Destination object mapped from source</returns>
        public T GetMappedValue<T>(object value, FoodOrderingContext dbContext)
        {
            return (T)Mapper.Map(value, value.GetType(), typeof(T), o => o.Items["dbContext"] = dbContext);
        }
    }
}
