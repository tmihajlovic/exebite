using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;

namespace FeatureTestingConsole
{
    public sealed class App : IApp
    {
        private readonly IRestaurantCommandRepository _restaurantCommandRepository;
        private readonly ILocationCommandRepository _locationCommandRepo;
        private readonly IMealOrderingContextFactory _factory;

        public App(
            IRestaurantCommandRepository restaurantCommandRepo,
            ILocationCommandRepository locationCommandRepo,
            IMealOrderingContextFactory factory)
        {
            _restaurantCommandRepository = restaurantCommandRepo;
            _locationCommandRepo = locationCommandRepo;
            _factory = factory;
        }

        public void Run(string[] args)
        {
            ResetDatabase();

            SeedLocation();

            SeedRestaurant();
        }

        private void ResetDatabase()
        {
            using (var dc = _factory.Create())
            {
                dc.Database.EnsureDeleted();
                dc.Database.EnsureCreated();
            }
        }

        private void SeedLocation()
        {
            _locationCommandRepo.Insert(new LocationInsertModel()
            {
                Name = "BVS",
                Address = "Vojvode Stepe 50"
            });

            _locationCommandRepo.Insert(new LocationInsertModel()
            {
                Name = "MM",
                Address = "Đorđa Rajkovića 2"
            });
        }

        private void SeedRestaurant()
        {
            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = RestaurantConstants.POD_LIPOM_NAME
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = RestaurantConstants.TOPLI_OBROK_NAME
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = RestaurantConstants.MIMAS_NAME
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = RestaurantConstants.INDEX_NAME
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = RestaurantConstants.SERPICA_NAME
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = RestaurantConstants.HEY_DAY_NAME
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = RestaurantConstants.PARRILLA_NAME
            });
        }
    }
}
