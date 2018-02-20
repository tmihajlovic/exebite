using System.Collections.Generic;
using System.Data.Entity;
using Exebite.DataAccess.Context;
using Exebite.Model;

namespace Exebite.DataAccess.Handlers
{
    public class RecipeHandler : IRecipeHandler
    {
        IFoodOrderingContextFactory _factory;

        public RecipeHandler(IFoodOrderingContextFactory factory)
        {
            _factory = factory;
        }

        public void Delete(int Id)
        {
            using (var context = _factory.Create())
            {
                var recipe = context.Recipes.Find(Id);
                context.Recipes.Remove(recipe);
                context.SaveChanges();
            }
        }

        public IEnumerable<Recipe> Get()
        {
            using (var context = _factory.Create())
            {
                var recipeEntities = new List<Recipe>();

                foreach (var recipe in context.Recipes)
                {
                    var recipeModel = AutoMapperHelper.Instance.GetMappedValue<Recipe>(recipe);
                    recipeEntities.Add(recipeModel);
                }

                return recipeEntities;
            }
        }

        public Recipe GetByID(int Id)
        {
            using (var context = _factory.Create())
            {
                var recipeEntity = context.Recipes.Find(Id);
                var recipe = AutoMapperHelper.Instance.GetMappedValue<Recipe>(recipeEntity);
                return recipe;
            }
        }

        public void Insert(Recipe entity)
        {
            using (var context = _factory.Create())
            {
                var recipeEntity = AutoMapperHelper.Instance.GetMappedValue<RecipeEntity>(entity);

                context.Recipes.Add(recipeEntity);
                context.SaveChanges();
            }
        }

        public void Update(Recipe entity)
        {
            using (var context = _factory.Create())
            {
                var recipeEntity = AutoMapperHelper.Instance.GetMappedValue<RecipeEntity>(entity);
                context.Entry(recipeEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
