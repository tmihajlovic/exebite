using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;

namespace FeatureTestingConsole
{
    public sealed class App : IApp
    {
        private readonly IRestaurantCommandRepository _restaurantCommandRepository;
        private readonly IRoleCommandRepository _roleCommandRepo;
        private readonly ILocationCommandRepository _locationCommandRepo;
        private readonly IFoodOrderingContextFactory _factory;

        public App(
            IRestaurantCommandRepository restaurantCommandRepo,
            ILocationCommandRepository locationCommandRepo,
            IFoodOrderingContextFactory factory,
            IRoleCommandRepository roleCommandRepo)
        {
            _restaurantCommandRepository = restaurantCommandRepo;
            _locationCommandRepo = locationCommandRepo;
            _factory = factory;
            _roleCommandRepo = roleCommandRepo;
        }

        public void Run(string[] args)
        {
            ResetDatabase();

            SeedRole();
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

        private void SeedRole()
        {
            _roleCommandRepo.Insert(new RoleInsertModel { Name = "Admin" });
            _roleCommandRepo.Insert(new RoleInsertModel { Name = "User" });
        }

        private void SeedLocation()
        {
            _locationCommandRepo.Insert(new LocationInsertModel()
            {
                Name = "Execom VS",
                Address = "Vojvode Stepe 50"
            });

            _locationCommandRepo.Insert(new LocationInsertModel()
            {
                Name = "Execom MM",
                Address = "Đorđa Rajkovica 2"
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
