using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Repositories;
using Exebite.Model;

namespace Exebite.Business
{
    public class FoodService : IFoodService
    {
        private IFoodRepository _foodHandler;

        public FoodService(IFoodRepository foodHandler)
        {
            _foodHandler = foodHandler;
        }

        public List<Food> GetAllFoods()
        {
            return _foodHandler.GetAll().ToList();
        }

        public Food GetFoodById(int id)
        {
            return _foodHandler.GetByID(id);
        }

        public Food CreateNewFood(Food food)
        {
            if (food == null)
            {
                throw new System.ArgumentNullException(nameof(food));
            }

            return _foodHandler.Insert(food);
        }

        public Food UpdateFood(Food food)
        {
            return _foodHandler.Update(food);
        }

        public void Delete(int foodId)
        {
            var food = _foodHandler.GetByID(foodId);
            if (food != null)
            {
                _foodHandler.Delete(foodId);
            }
        }
    }
}
