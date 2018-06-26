using System.Collections.Generic;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using System.Linq;

namespace FeatureTestingConsole
{
    public sealed class App : IApp
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IRestaurantRepository _restaurantRepo;
        private readonly IFoodRepository _foodRepository;
        private readonly ICustomerRepository _customerRepo;
        private readonly ILocationRepository _locationRepo;

        private readonly IMealRepository _mealRepo;

        public App(IOrderRepository orderRepository, IRestaurantRepository restaurantRepo, IFoodRepository foodRepository, ICustomerRepository customerRepository, ILocationRepository locationRepo, IMealRepository mealRepo)
        {
            _orderRepo = orderRepository;
            _restaurantRepo = restaurantRepo;
            _foodRepository = foodRepository;
            _customerRepo = customerRepository;
            _locationRepo = locationRepo;
            _mealRepo = mealRepo;
        }

        public void Run(string[] args)
        {
            _restaurantRepo.Insert(new Restaurant()
            {
                Name = "Test restaurant"
            });

            _locationRepo.Insert(new Location()
            {
                Name = "Execom",
                Address = "Vojvode stepe 50"
            });

            _customerRepo.Insert(new Customer()
            {
                Name = "Test restaurant",
                AppUserId = "1514586545614 546456",
                Balance = 2000m,
                LocationId = 1
            });

            _foodRepository.Insert(new Food()
            {
                Name = "Supa",
                Price = 100,
                RestaurantId = 1
            });

            _foodRepository.Insert(new Food()
            {
                Name = "Kaubojska pasta",
                Price = 100,
                RestaurantId = 1
            });


            var foods = _foodRepository.Query(new FoodQueryModel() { });

            _mealRepo.Insert(new Meal()
            {
                Foods = foods.Select(x => new Food() { Id = x.Id }).ToList()
            });

            var order = new Order()
            {
                CustomerId = 1,
                MealId = 1,
                Note = "Test insert",
                Price = 150
            };

            this._orderRepo.Insert(order);
        }
    }
}
