using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Either;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;

namespace FeatureTestingConsole
{
    public sealed class App : IApp
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IRestaurantQueryRepository _restaurantQueryRepository;
        private readonly IRestaurantCommandRepository _restaurantCommandRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly ICustomerCommandRepository _customerCommandRepo;
        private readonly ILocationCommandRepository _locationCommandRepo;
        private readonly IDailyMenuRepository _dailyMenu;
        private readonly IMealRepository _mealRepo;
        private readonly IFoodOrderingContextFactory factory;
        private readonly IMapper _mapper;

        public App(
            IOrderRepository orderRepository,
            IRestaurantQueryRepository restaurantQueryRepo,
            IRestaurantCommandRepository restaurantCommandRepo,
            IFoodRepository foodRepository,
            ICustomerCommandRepository customerCommandRepo,
            ILocationCommandRepository locationCommandRepo,
            IMealRepository mealRepo,
            IFoodOrderingContextFactory factory,
            IDailyMenuRepository dailyMenu,
            IMapper mapper)
        {
            _orderRepo = orderRepository;
            _restaurantQueryRepository = restaurantQueryRepo;
            _restaurantCommandRepository = restaurantCommandRepo;
            _foodRepository = foodRepository;
            _customerCommandRepo = customerCommandRepo;
            _locationCommandRepo = locationCommandRepo;
            _mealRepo = mealRepo;
            this.factory = factory;
            _dailyMenu = dailyMenu;
            _mapper = mapper;
        }

        public void Run(string[] args)
        {
            ResetDatabase();

            // liefs
            SeedRestaurant();
            SeedLocation();

            SeedCustomer(); // restaurant related
            SeedPayment();
            SeedFoods();
            SeedMeal();

            SeedDailyMenu();

            var restaurant = _restaurantQueryRepository.Query(new RestaurantQueryModel { Id = 1 })
                                                       .Map(x => x.Items.First())
                                                       .Reduce(_ => throw new System.Exception());

            _restaurantCommandRepository.Update(restaurant.Id, _mapper.Map<RestaurantUpdateModel>(restaurant));

            var order = new Order()
            {
                CustomerId = 1,
                MealId = 1,
                Note = "Test insert"
            };

            this._orderRepo.Insert(order);
        }

        private void SeedPayment()
        {
            using (var dc = factory.Create())
            {
                dc.Payment.Add(new PaymentEntity()
                {
                    Amount = 2000,
                    CustomerId = 1,
                });
                dc.SaveChanges();
            }
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
            _customerCommandRepo.Insert(new CustomerInsertModel
            {
                Name = "Customer 1",
                AppUserId = "1514586545614 546456",
                Balance = 2000m,
                LocationId = 1
            });

            _customerCommandRepo.Insert(new CustomerInsertModel
            {
                Name = "Customer 2",
                AppUserId = "1514586545614 546456",
                Balance = -400m,
                LocationId = 2
            });
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
                Name = "Lipa restaurant"
            });

            _restaurantCommandRepository.Insert(new RestaurantInsertModel()
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
