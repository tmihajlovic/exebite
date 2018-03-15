using System.Collections.Generic;
using Exebite.Model;

namespace Exebite.DataAccess
{
    public interface IRecipeRepository : IDatabaseRepository<Recipe>
    {
        // Add functions specific for IRecipeHandler

        /// <summary>
        /// Get recipes where given food is main course
        /// </summary>
        /// <param name="mainCourse"><see cref="Food"/> od main course</param>
        /// <returns>Lisr of recipes </returns>
        List<Recipe> GetRecipesForMainCourse(Food mainCourse);

        /// <summary>
        /// Get recipies containing given food
        /// </summary>
        /// <param name="food">Food for recepie search</param>
        /// <returns>List of recipes</returns>
        List<Recipe> GetRecipesForFood(Food food);
    }
}
