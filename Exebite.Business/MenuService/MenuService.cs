using System;
using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;

namespace Exebite.Business
{
    public class MenuService : IMenuService
    {
        private readonly IRestaurantQueryRepository _restaurantRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IRecipeRepository _recipeRepository;

        public MenuService(IRestaurantQueryRepository restaurantRepository, IFoodRepository foodRepository, IRecipeRepository recipeRepository)
        {
            _restaurantRepository = restaurantRepository;
            _foodRepository = foodRepository;
            _recipeRepository = recipeRepository;
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

        public List<Food> CheckAvailableSideDishes(int foodId)
        {
            var food = _foodRepository.GetByID(foodId);
            if (food == null)
            {
                throw new ArgumentException("Non existing food");
            }

            var allRecipe = _recipeRepository.Get(0, int.MaxValue);
            var foodRecipe = allRecipe.SingleOrDefault(r => r.MainCourse.Id == food.Id);
            if (foodRecipe != null)
            {
                return foodRecipe.SideDish;
            }

            return new List<Food>();
        }
    }
}
