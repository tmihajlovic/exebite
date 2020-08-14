using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Option;

namespace Exebite.Business
{
    public class MenuService : IMenuService
    {
        private readonly IRestaurantQueryRepository _restaurantRepository;
        private readonly IMealQueryRepository _mealQueryRepository;

        public MenuService(IRestaurantQueryRepository restaurantRepository, IMealQueryRepository mealQueryRepository)
        {
            _restaurantRepository = restaurantRepository;
            _mealQueryRepository = mealQueryRepository;
        }

        public Either<Error, PagingResult<Restaurant>> GetRestorantsWithMenus()
        {
            return _restaurantRepository.Query(new RestaurantQueryModel());
        }

        public decimal CheckPrice(Meal meal)
        {
            // TODO: when special offers are done implement check
            throw new NotImplementedException();
        }

        public IList<Meal> GetCondimentsForMeal(long mealId)
        {
            return _mealQueryRepository.GetCondimentsForMeal(new MealQueryModel { Id = mealId })
                .Map(c => c.Items.ToList())
                .Reduce(_ => throw new Exception());
        }
    }
}
