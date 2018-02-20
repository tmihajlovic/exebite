using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class MenuService : IMenuService
    {
        IRestaurantHandler _restaurantHandler;
        IFoodHandler _foodHandler;
        IRecipeHandler _recipeHandler;

        public MenuService(IRestaurantHandler restaurantHandler, IFoodHandler foodHandler, IRecipeHandler recipeHandler)
        {
            _restaurantHandler = restaurantHandler;
            _foodHandler = foodHandler;
            _recipeHandler = recipeHandler;
        }

        public List<Restaurant> GetRestorantsWithMenus()
        {
            return _restaurantHandler.Get().ToList();
        }

        public int CheckPrice(Meal meal)
        {
            //TODO: when special offers are done implement check 
            throw new NotImplementedException();
        }

        public List<Food> CheckAvailableSideDishes(int foodId)
        {
            var food = _foodHandler.GetByID(foodId);
            var AllRecipe = _recipeHandler.Get();
            return AllRecipe.SingleOrDefault(r => r.MainCourse == food).SideDish;
        }
    }
}
