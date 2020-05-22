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
                Address = "Vojvode stepe 50"
            });

            _locationCommandRepo.Insert(new LocationInsertModel()
            {
                Name = "Execom JD",
                Address = "Jovana ducica 50"
            });
        }

        private void SeedRestaurant()
        {
            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = "Pod Lipom"
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = "Topli Obrok"
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = "Mimas"
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = "Index House"
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = "Serpica"
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = "Hey Day"
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
            {
                Name = "Parrilla"
            });
        }
    }
}
