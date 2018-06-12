using System;
using System.Collections.Generic;
using Exebite.DataAccess.Repositories;
using Exebite.Model;

namespace Exebite.Business
{
    public class RecipeService : IRecipeService
    {
        private IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IList<Recipe> Get(int page, int size)
        {
            return _recipeRepository.Get(page, size);
        }

        public Recipe GetById(int recipeId)
        {
            return _recipeRepository.GetByID(recipeId);
        }

        public Recipe Update(Recipe recipe)
        {
            return _recipeRepository.Update(recipe);
        }

        public Recipe Create(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }

            return _recipeRepository.Insert(recipe);
        }

        public void Delete(int recipeId)
        {
            var recipe = _recipeRepository.GetByID(recipeId);
            if (recipe != null)
            {
                _recipeRepository.Delete(recipeId);
            }
        }
    }
}
