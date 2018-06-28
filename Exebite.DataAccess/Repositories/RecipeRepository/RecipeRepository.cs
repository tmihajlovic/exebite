using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Exebite.DataAccess.Repositories
{
    public class RecipeRepository : DatabaseRepository<Recipe, RecipeEntity, RecipeQueryModel>, IRecipeRepository
    {
        public RecipeRepository(IFoodOrderingContextFactory factory, IMapper mapper, ILogger<RecipeRepository> logger)
            : base(factory, mapper, logger)
        {
        }

        public List<Recipe> GetRecipesForFood(Food food)
        {
            if (food == null)
            {
                throw new System.ArgumentNullException(nameof(food));
            }

            using (var context = _factory.Create())
            {
                var entities = context.Foods.Where(fe => fe.Id == food.Id).SelectMany(r => r.FoodEntityRecipeEntities.Select(x => x.RecipeEntity)).ToList();
                return entities.Select(r => _mapper.Map<Recipe>(r)).ToList();
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
                return entities.Select(r => _mapper.Map<Recipe>(r)).ToList();
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
                var recipeEntity = new RecipeEntity
                {
                    Id = entity.Id,
                    RestaurantId = entity.RestaurantId,
                    MainCourseId = entity.MainCourseId,
                    FoodEntityRecipeEntities = entity.SideDish
                                                     .Select(a => new FoodEntityRecipeEntity { FoodEntityId = a.Id })
                                                     .ToList()
                };
                var createEntity = context.Add(recipeEntity).Entity;
                context.SaveChanges();

                var result = context.Recipes
                                    .Include(r => r.FoodEntityRecipeEntities)
                                    .Include(r => r.MainCourse)
                                    .FirstOrDefault(r => r.Id == createEntity.Id);
                return _mapper.Map<Recipe>(createEntity);
            }
        }

        public override IList<Recipe> Query(RecipeQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new System.ArgumentNullException("queryModel can't be null");
            }

            using (var context = _factory.Create())
            {
                var query = context.Recipes.AsQueryable();

                if (queryModel.Id != null)
                {
                    query = query.Where(x => x.Id == queryModel.Id.Value);
                }

                var results = query.ToList();
                return _mapper.Map<IList<Recipe>>(results);
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
                var currentEntity = context.Recipes.Find(entity.Id);
                currentEntity.MainCourseId = entity.MainCourseId;
                currentEntity.RestaurantId = entity.RestaurantId;

                // this will remove old references, and after that new ones will be added
                var addedEntities = Enumerable.Range(0, entity.SideDish.Count).Select(a =>
                {
                    return new FoodEntityRecipeEntity { FoodEntityId = entity.SideDish[a].Id, RecepieEntityId = entity.Id };
                }).ToList();

                var deletedEntities = currentEntity.FoodEntityRecipeEntities.Except(addedEntities).ToList();

                deletedEntities.ForEach(d => currentEntity.FoodEntityRecipeEntities.Remove(d));

                addedEntities.ForEach(a => currentEntity.FoodEntityRecipeEntities.Add(a));

                context.SaveChanges();

                var resultEntity = context.Recipes.FirstOrDefault(r => r.Id == entity.Id);

                return _mapper.Map<Recipe>(resultEntity);
            }
        }
    }
}
