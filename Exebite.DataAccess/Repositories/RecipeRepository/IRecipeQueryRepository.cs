using System.Collections.Generic;
using Either;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IRecipeQueryRepository : IDatabaseQueryRepository<Recipe, RecipeQueryModel>
    {
        /// <summary>
        /// Get recipies containing given food
        /// </summary>
        /// <param name="foodId">Food for recepie search</param>
        /// <returns>List of recipes</returns>
        Either<Error, IList<Recipe>> GetRecipesForFood(int foodId);
    }
}
