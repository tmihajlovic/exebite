using System.Collections.Generic;
using Exebite.DomainModel;
using Either;


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
