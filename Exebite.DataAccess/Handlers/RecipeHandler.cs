using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.Model;

namespace Exebite.DataAccess.Handlers
{
    public class RecipeHandler  :DatabaseHandler<Recipe, RecipeEntity>, IRecipeHandler
    {
        IFoodOrderingContextFactory _factory;

        public RecipeHandler(IFoodOrderingContextFactory factory)
            :base(factory)
        {
            _factory = factory;
        }


        public override Recipe Insert(Recipe entity)
        {
            using (var context = _factory.Create())
            {
                var recipeEntity = AutoMapperHelper.Instance.GetMappedValue<RecipeEntity>(entity);
                recipeEntity.MainCourse = context.Foods.SingleOrDefault(f => f.Id == recipeEntity.MainCourse.Id);
                for(int i = 0; i < recipeEntity.Foods.Count; i++)
                {
                    recipeEntity.Foods[i] = context.Foods.SingleOrDefault(f => f.Id == recipeEntity.Foods[i].Id);
                }

                var resultEntity = context.Recipes.Add(recipeEntity);
                context.SaveChanges();
                var result = AutoMapperHelper.Instance.GetMappedValue<Recipe>(resultEntity);
                return result;
            }
        }

        public override Recipe Update(Recipe entity)
        {
            using (var context = _factory.Create())
            {
                var recipeEntity = AutoMapperHelper.Instance.GetMappedValue<RecipeEntity>(entity);
                var oldRecipeEntity = context.Recipes.FirstOrDefault(r => r.Id == entity.Id);
                context.Entry(oldRecipeEntity).CurrentValues.SetValues(recipeEntity);

                oldRecipeEntity.MainCourse = context.Foods.SingleOrDefault(f => f.Id == oldRecipeEntity.MainCourse.Id);
                for (int i = 0; i < oldRecipeEntity.Foods.Count; i++)
                {
                    oldRecipeEntity.Foods[i] = context.Foods.SingleOrDefault(f => f.Id == oldRecipeEntity.Foods[i].Id);
                }

                
                context.SaveChanges();

                var resultEntity = context.Recipes.FirstOrDefault(r => r.Id == entity.Id);
                var result = AutoMapperHelper.Instance.GetMappedValue<Recipe>(resultEntity);
                return result;
            }
        }
    }
}
