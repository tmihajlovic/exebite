using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class MenuService : IMenuService
    {
        private IRestaurantRepository _restaurantHandler;
        private IFoodRepository _foodHandler;
        private IRecipeRepository _recipeHandler;

        public MenuService(IRestaurantRepository restaurantHandler, IFoodRepository foodHandler, IRecipeRepository recipeHandler)
        {
            _restaurantHandler = restaurantHandler;
            _foodHandler = foodHandler;
            _recipeHandler = recipeHandler;
        }

        public List<Restaurant> GetRestorantsWithMenus()
        {
            return _restaurantHandler.GetAll().ToList();
        }

        public decimal CheckPrice(Meal meal)
        {
            // TODO: when special offers are done implement check
            throw new NotImplementedException();
        }

        public List<Food> CheckAvailableSideDishes(int foodId)
        {
            var food = _foodHandler.GetByID(foodId);
            var allRecipe = _recipeHandler.GetAll();
            return allRecipe.SingleOrDefault(r => r.MainCourse.Id == food.Id).SideDish;
        }
    }
}
