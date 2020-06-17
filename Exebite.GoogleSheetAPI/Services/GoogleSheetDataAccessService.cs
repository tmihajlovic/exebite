using System;
using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.Services
{
    /// <inheritdoc/>
    public sealed class GoogleSheetDataAccessService : IGoogleSheetDataAccessService
    {
        private readonly IEitherMapper _mapper;
        private readonly ICustomerCommandRepository _customerCommandRepository;
        private readonly ICustomerQueryRepository _customerQueryRepository;
        private readonly IMealQueryRepository _mealQueryRepository;
        private readonly IMealCommandRepository _mealCommandRepository;
        private readonly IDailyMenuQueryRepository _dailyMenuQueryRepository;
        private readonly IDailyMenuCommandRepository _dailyMenuCommandRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleSheetDataAccessService"/> class.
        /// </summary>
        /// <param name="mapper">Mapper instance.</param>
        /// <param name="customerCommandRepository">Customer command repository</param>
        /// <param name="customerQueryRepository">Customer query repository.</param>
        /// <param name="foodCommandRepository">Food command repository</param>
        /// <param name="foodQueryRepository">Food query repository.</param>
        /// <param name="dailyMenuCommandRepository">Daily menu command repository.</param>
        /// <param name="dailyMenuQueryRepository">Daily menu query repository.</param>
        public GoogleSheetDataAccessService(
            IEitherMapper mapper,
            ICustomerCommandRepository customerCommandRepository,
            ICustomerQueryRepository customerQueryRepository,
            IMealCommandRepository foodCommandRepository,
            IMealQueryRepository foodQueryRepository,
            IDailyMenuCommandRepository dailyMenuCommandRepository,
            IDailyMenuQueryRepository dailyMenuQueryRepository)
        {
            _mapper = mapper;

            _customerCommandRepository = customerCommandRepository;
            _customerQueryRepository = customerQueryRepository;
            _mealCommandRepository = foodCommandRepository;
            _mealQueryRepository = foodQueryRepository;
            _dailyMenuCommandRepository = dailyMenuCommandRepository;
            _dailyMenuQueryRepository = dailyMenuQueryRepository;
        }

        /// <inheritdoc/>
        public Either<Error, (int Added, int Updated)> UpdateCustomers(IEnumerable<Customer> customers)
        {
            var added = 0;
            var updated = 0;

            if (!customers.Any())
            {
                return new Left<Error, (int, int)>(new ArgumentNotSet(nameof(customers)));
            }

            foreach (var customer in customers)
            {
                var exists = _customerQueryRepository
                     .ExistsByGoogleId(customer.GoogleUserId)
                     .Reduce(r => false, ex => Console.WriteLine(ex.ToString()));

                if (!exists)
                {
                    added += _mapper
                        .Map<CustomerInsertModel>(customer)
                        .Map(_customerCommandRepository.Insert)
                        .Reduce(r => 0, ex => Console.WriteLine(ex.ToString())) > 0 ? 1 : 0;
                }
                else
                {
                    updated += _mapper
                        .Map<CustomerUpdateModel>(customer)
                        .Map(_customerCommandRepository.UpdateByGoogleId)
                        .Reduce(r => false, ex => Console.WriteLine(ex.ToString())) ? 1 : 0;
                }
            }

            return new Right<Error, (int, int)>((added, updated));
        }

        /// <inheritdoc/>
        public Either<Error, (int Added, int Updated)> UpdateFoods(IEnumerable<Meal> meals)
        {
            var added = 0;
            var updated = 0;

            var updatedFood = new List<Meal>();

            if (!meals.Any())
            {
                return new Left<Error, (int, int)>(new ArgumentNotSet(nameof(meals)));
            }

            DailyMenu dailyMenu = GetDailyMenu(meals.First().Restaurant.Id);
            if (dailyMenu == null)
            {
                return new Left<Error, (int, int)>(new ArgumentNotSet(nameof(dailyMenu)));
            }

            foreach (var food in meals)
            {
                var mealId = _mealQueryRepository
                            .FindByNameAndRestaurantId(new MealQueryModel { Name = food.Name, RestaurantId = food.Restaurant.Id })
                            .Reduce(r => 0, ex => Console.WriteLine(ex.ToString()));

                var exists = mealId > 0 ? true : false;

                if (!exists)
                {
                    var insertFood = new MealInsertModel
                    {
                        Description = food.Description,
                        IsActive = food.IsActive,
                        Name = food.Name,
                        Price = food.Price,
                        RestaurantId = food.Restaurant.Id,
                        Type = (MealType)food.Type
                    };

                    mealId = _mapper
                        .Map<MealInsertModel>(insertFood)
                        .Map(_mealCommandRepository.Insert)
                        .Reduce(r => 0, ex => Console.WriteLine(ex.ToString()));

                    added += mealId > 0 ? 1 : 0;
                }
                else
                {
                    updated += _mapper
                        .Map<MealUpdateModel>(food)
                        .Map(_mealCommandRepository.UpdateByNameAndRestaurantId)
                        .Reduce(r => false, ex => Console.WriteLine(ex.ToString())) ? 1 : 0;
                }

                food.Id = mealId;
            }

            _dailyMenuCommandRepository.Update(dailyMenu.Id, new DailyMenuUpdateModel() { RestaurantId = dailyMenu.Restaurant.Id, Date = dailyMenu.Date, Meals = meals.ToList() });

            return new Right<Error, (int, int)>((added, updated));
        }

        /// <summary>
        /// Gets the daily menu id from database.
        /// </summary>
        /// <param name="restaurantId">Restaurant ID for which we are look for daily menu.</param>
        /// <returns>The ID of the daily menu.</returns>
        private DailyMenu GetDailyMenu(long restaurantId)
        {
            var dailyMenus = _dailyMenuQueryRepository
                                .Query(new DailyMenuQueryModel { RestaurantId = restaurantId })
                                .Map(d => d.Items)
                                .Reduce(r => new List<DailyMenu>(), ex => Console.WriteLine(ex.ToString()));

            if (dailyMenus.Any())
            {
                return dailyMenus.First();
            }

            var dailyMenuId = _dailyMenuCommandRepository
                                .Insert(new DailyMenuInsertModel { RestaurantId = restaurantId, Date = DateTime.Today })
                                .Map(d => d)
                                .Reduce(r => 0, ex => Console.WriteLine(ex.ToString()));

            dailyMenus = _dailyMenuQueryRepository
                                .Query(new DailyMenuQueryModel { Id = dailyMenuId })
                                .Map(d => d.Items)
                                .Reduce(r => new List<DailyMenu>(), ex => Console.WriteLine(ex.ToString()));

            return dailyMenus.First();
        }
    }
}