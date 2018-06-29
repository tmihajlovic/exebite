using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;

namespace FeatureTestingConsole
{
    public sealed class App : IApp
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IRestaurantRepository _restaurantRepo;
        private readonly IFoodRepository _foodRepository;
        private readonly ICustomerRepository _customerRepo;
        private readonly ILocationRepository _locationRepo;
        private readonly IDailyMenuRepository _dailyMenu;
        private readonly IMealRepository _mealRepo;
        private readonly IFoodOrderingContextFactory factory;

        public App(IOrderRepository orderRepository, IRestaurantRepository restaurantRepo, IFoodRepository foodRepository, ICustomerRepository customerRepository, ILocationRepository locationRepo, IMealRepository mealRepo, IFoodOrderingContextFactory factory, IDailyMenuRepository dailyMenu)
        {
            _orderRepo = orderRepository;
            _restaurantRepo = restaurantRepo;
            _foodRepository = foodRepository;
            _customerRepo = customerRepository;
            _locationRepo = locationRepo;
            _mealRepo = mealRepo;
            this.factory = factory;
            _dailyMenu = dailyMenu;
        }

        public void Run(string[] args)
        {
            ResetDatabase();

            // leeefs
            SeedRestaurant();
            SeedLocation();

            // restaurant related
            SeedFoods();

            SeedCustomer();

            SeedMeal();


            SeedDailyMenu();

            var restaurant = _restaurantRepo.GetByID(1);

            _restaurantRepo.Update(restaurant);



            var order = new Order()
            {
                CustomerId = 1,
                MealId = 1,
                Note = "Test insert"

            };

            this._orderRepo.Insert(order);
        }

        private void ResetDatabase()
        {
            using (var dc = factory.Create())
            {
                dc.Database.EnsureDeleted();
                dc.Database.EnsureCreated();
            }
        }

        private void SeedCustomer()
        {
            _customerRepo.Insert(new Customer()
            {
                Name = "Customer 1",
                AppUserId = "1514586545614 546456",
                Balance = 2000m,
                LocationId = 1

            });

            _customerRepo.Insert(new Customer()
            {
                Name = "Customer 2",
                AppUserId = "1514586545614 546456",
                Balance = -400m,
                LocationId = 2
            });
        }

        private void SeedLocation()
        {
            _locationRepo.Insert(new Location()
            {
                Name = "Execom VS",
                Address = "Vojvode stepe 50"
            });

            _locationRepo.Insert(new Location()
            {
                Name = "Execom JD",
                Address = "Jovana ducica 50"
            });
        }

        private void SeedRestaurant()
        {
            _restaurantRepo.Insert(new Restaurant()
            {
                Name = "Lipa restaurant"
            });

            _restaurantRepo.Insert(new Restaurant()
            {
                Name = "Hedone restaurant"
            });
        }

        private void SeedFoods()
        {
            // Lipa
            _foodRepository.Insert(new Food()
            {
                Name = "Supa",
                Price = 100,
                RestaurantId = 1,
                Type = FoodType.SOUP
            });
            // lipa
            _foodRepository.Insert(new Food()
            {
                Name = "Kaubojska pasta",
                Price = 100,
                RestaurantId = 1,
                Type = FoodType.MAIN_COURSE
            });

            // Hedone
            _foodRepository.Insert(new Food()
            {
                Name = "Ramstek",
                Price = 400,
                RestaurantId = 2,
                Type = FoodType.MAIN_COURSE
            });

            // Hedone
            _foodRepository.Insert(new Food()
            {
                Name = "Princes krofna",
                Price = 100,
                RestaurantId = 2,
                Type = FoodType.DESERT
            });
        }

        private void SeedMeal()
        {

            _mealRepo.Insert(new Meal()
            {
                Foods = new List<Food>()
                {
                    new Food()
                    {
                        Id = 1
                    },
                    new Food()
                    {
                        Id = 2
                    }
                },
                Price = 400m
            });
        }

        private void SeedDailyMenu()
        {
            _dailyMenu.Insert(new DailyMenu()
            {
                Foods = new List<Food>()
                {
                    new Food()
                    {
                        Id = 1
                    },
                    new Food()
                    {
                        Id = 2
                    }
                },
                RestaurantId = 1
            });

            _dailyMenu.Insert(new DailyMenu()
            {
                Foods = new List<Food>()
                {
                    new Food()
                    {
                        Id = 1
                    }
                },
                RestaurantId = 2
            });

        }
    }
}
