using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Repositories
{
    public class RecipeRepository : DatabaseRepository<Recipe, RecipeEntity, RecipeQueryModel>, IRecipeRepository
    {
        public RecipeRepository(IFoodOrderingContextFactory factory, IMapper mapper)
            : base(factory, mapper)
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
                    MainCourseId = entity.MainCourse.Id,
                    FoodEntityRecipeEntities = Enumerable.Range(0, entity.SideDish.Count).Select(a =>
                    {
                        return new FoodEntityRecipeEntity { FoodEntityId = entity.SideDish[a].Id };
                    }).ToList()
                };
                var createEntity = context.Attach(recipeEntity).Entity;
                context.SaveChanges();

                createEntity = context.Recipes.Include(r => r.FoodEntityRecipeEntities)
                                    .Include(r => r.MainCourse)
                                    .First(r => r.Id == createEntity.Id);
                return _mapper.Map<Recipe>(createEntity);
            }
        }

        public override IList<Recipe> Query(RecipeQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new System.ArgumentException("queryModel can't be null");
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
                // todo: must be like this. Do not use mapper - Remove comment when all repositories are finished
                var currentEntity = context.Recipes.Find(entity.Id);
                currentEntity.MainCourseId = entity.MainCourse.Id;

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
