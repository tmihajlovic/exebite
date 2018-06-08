using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class RecipeRepository : DatabaseRepository<Recipe, RecipeEntity>, IRecipeRepository
    {
        private readonly IFoodOrderingContextFactory _factory;


        public RecipeRepository(IFoodOrderingContextFactory factory, IExebiteMapper mapper)
            : base(factory, mapper)
        {
            _factory = factory;
        }

        public List<Recipe> GetRecipesForFood(Food food)
        {
            if (food == null)
            {
                throw new System.ArgumentNullException(nameof(food));
            }

            using (var context = _factory.Create())
            {
                var entities = context.FoodEntityRecipeEntity.Where(fe => fe.FoodEntityId == food.Id).Select(r => r.RecipeEntity).ToList();
                return entities.Select(r => _exebiteMapper.Map<Recipe>(r)).ToList();
            }
        }

        public List<Recipe> GetRecipesForMainCourse(Food mainCourse)
        {
            if (mainCourse == null)
            {
                throw new System.ArgumentNullException(nameof(mainCourse));
            }

            using (var context = _factory.Create())
            {
                var entities = context.Recipes.Where(r => r.MainCourseId == mainCourse.Id).ToList();
                return entities.Select(r => _exebiteMapper.Map<Recipe>(r)).ToList();
            }
        }

        public override Recipe Insert(Recipe entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var recipeEntity = _exebiteMapper.Map<RecipeEntity>(entity);
                var resultEntity = context.Attach(recipeEntity).Entity;
                context.SaveChanges();
                return _exebiteMapper.Map<Recipe>(resultEntity);
            }
        }

        public override Recipe Update(Recipe entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var recipeEntity = AutoMapperHelper.Instance.GetMappedValue<RecipeEntity>(entity, context);
                foreach (var fre in recipeEntity.FoodEntityRecipeEntities)
                {
                    context.Attach(fre);
                }

                var old = context.Recipes.Find(entity.Id);
                context.Entry(old).CurrentValues.SetValues(recipeEntity);
                context.SaveChanges();

                var resultEntity = context.Recipes.FirstOrDefault(r => r.Id == entity.Id);
                return AutoMapperHelper.Instance.GetMappedValue<Recipe>(resultEntity, context);
            }
        }
    }
}
