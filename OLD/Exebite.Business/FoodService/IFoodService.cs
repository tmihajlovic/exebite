using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.Business
{
    public interface IFoodService
    {
        List<Food> GetAllFoods();
        Food GetFoodById(int Id);
        Food GetFoodByName(Food food);
        Food GetOrCreateFood(Food food);
        void CrateNewFood(Food food);
        void UpdateFood(Food food);
        void Delete(int foodId);
    }
}
