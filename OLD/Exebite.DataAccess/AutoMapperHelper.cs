using AutoMapper;
using Exebite.DataAccess.Entities;
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
                cfg.CreateMap(typeof(MealEntity), typeof(Meal));
                cfg.CreateMap(typeof(CustomerAliasesEntity), typeof(CustomerAliases));

                cfg.CreateMap(typeof(Customer), typeof(CustomerEntity));
                cfg.CreateMap<Customer, CustomerEntity>().ForMember(i => i.LocationId, option => option.MapFrom(c => c.Location.Id));
                cfg.CreateMap(typeof(Order), typeof(OrderEntity));
                cfg.CreateMap(typeof(Food), typeof(FoodEntity));
                cfg.CreateMap(typeof(Restaurant), typeof(RestaurantEntity));
                cfg.CreateMap(typeof(Location), typeof(LocationEntity));
                cfg.CreateMap(typeof(Recipe), typeof(RecipeEntity));
                cfg.CreateMap(typeof(Meal), typeof(MealEntity));
                cfg.CreateMap(typeof(CustomerAliases), typeof(CustomerAliasesEntity));
            });
        }

        /// <summary>
        /// Instance of the class
        /// </summary>
        public static AutoMapperHelper Instance
        {
            get
            {
                lock(_locker)
                {
                    if(_instance == null)
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
        /// <returns>Destination object mapped from source</returns>
        public T GetMappedValue<T>(object value)
        {
            return (T)Mapper.Map(value, value.GetType(), typeof(T));
        }
    }
}
