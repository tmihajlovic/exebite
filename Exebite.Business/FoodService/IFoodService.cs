using System.Collections.Generic;
using Exebite.Model;

namespace Exebite.Business
{
    public interface IFoodService
    {
        /// <summary>
        /// Get all foods in database
        /// </summary>
        /// <returns>List of all <see cref="Food"/> </returns>
        IList<Food> GetAllFoods();

        /// <summary>
        /// Gets <see cref="Food"/> with given Id
        /// </summary>
        /// <param name="id">Id of food</param>
        /// <returns>Food with given Id</returns>
        Food GetFoodById(int id);

        /// <summary>
        /// Create new <see cref="Food"/>
        /// </summary>
        /// <param name="food">new food from database</param>
        /// <returns>Created food from database</returns>
        Food CreateNewFood(Food food);

        /// <summary>
        /// Update food info
        /// </summary>
        /// <param name="food"><see cref="Food"/> with updated info</param>
        /// <returns>Updated food from database</returns>
        Food UpdateFood(Food food);

        /// <summary>
        /// Delete food from database
        /// </summary>
        /// <param name="foodId">Id of foood to be deleted</param>
        void Delete(int foodId);
    }
}
