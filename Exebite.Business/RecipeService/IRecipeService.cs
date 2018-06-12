using System;
using System.Collections.Generic;
using System.Text;
using Exebite.Model;

namespace Exebite.Business
{
    public interface IRecipeService
    {
        IList<Recipe> Get(int page, int size);

        Recipe GetById(int recipeId);

        Recipe Update(Recipe recipe);

        Recipe Create(Recipe recipe);

        void Delete(int recipeId);
    }
}
