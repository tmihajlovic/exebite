using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class FoodService : IFoodService
    {
        IFoodRepository _foodHandler;

        public FoodService(IFoodRepository foodHandler)
        {
            _foodHandler = foodHandler;
        }

        public List<Food> GetAllFoods()
        {
            return _foodHandler.Get().ToList();
        }

        public Food GetFoodById(int Id)
        {
            return _foodHandler.GetByID(Id);
        }

        public Food GetFoodByName(Food food)
        {
            var allFood = this.GetAllFoods();
            return allFood.SingleOrDefault(f => f.Name == food.Name && f.Restaurant.Id == food.Restaurant.Id);
        }

        public Food GetOrCreateFood(Food food)
        {
            var foodFromDB = this.GetFoodByName(food);
            if(foodFromDB != null)
            {
                return foodFromDB;
            }
            else
            {
                _foodHandler.Insert(foodFromDB);
                return this.GetFoodByName(food);
            }
        }

        public void CrateNewFood(Food food)
        {
            _foodHandler.Insert(food);
        }

        public void UpdateFood(Food food)
        {
            _foodHandler.Update(food);
        }

        public void Delete(int foodId)
        {
            _foodHandler.Delete(foodId);
        }

    }
}
