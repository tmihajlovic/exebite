using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;

namespace Exebite.Business
{
    public class MenuService : IMenuService
    {
        private readonly IRestaurantRepository _restaurantHandler;
        private readonly IFoodRepository _foodHandler;
        private readonly IRecipeRepository _recipeHandler;

        public MenuService(IRestaurantRepository restaurantHandler, IFoodRepository foodHandler, IRecipeRepository recipeHandler)
        {
            _restaurantHandler = restaurantHandler;
            _foodHandler = foodHandler;
            _recipeHandler = recipeHandler;
        }

        public IList<Restaurant> GetRestorantsWithMenus()
        {
            return _restaurantHandler.Get(0, int.MaxValue);
        }

        public decimal CheckPrice(Meal meal)
        {
            // TODO: when special offers are done implement check
            throw new NotImplementedException();
        }

        public List<Food> CheckAvailableSideDishes(int foodId)
        {
            var food = _foodHandler.GetByID(foodId);
            if (food == null)
            {
                throw new ArgumentException("Non existing food");
            }

            var allRecipe = _recipeHandler.Get(0, int.MaxValue);
            var foodRecipe = allRecipe.SingleOrDefault(r => r.MainCourse.Id == food.Id);
            if (foodRecipe != null)
            {
                return foodRecipe.SideDish;
            }

            return new List<Food>();
        }
    }
}
