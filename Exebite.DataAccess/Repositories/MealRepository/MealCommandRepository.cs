using System;
using System.Linq;
using AutoMapper;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Repositories
{
    public class MealCommandRepository : IMealCommandRepository
    {
        private readonly IMapper _mapper;
        private readonly IFoodOrderingContextFactory _factory;

        public MealCommandRepository(IFoodOrderingContextFactory factory, IMapper mapper)
        {
            _mapper = mapper;
            _factory = factory;
        }

        public Either<Error, int> Insert(MealInsertModel entity)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var mealEntity = new MealEntity
                    {
                        Price = entity.Price,
                        FoodEntityMealEntities = entity.Foods.Select(x => new FoodEntityMealEntity { FoodEntityId = x }).ToList(),
                    };
                    var addedEntity = context.Add(mealEntity).Entity;
                    context.SaveChanges();
                    return new Right<Error, int>(addedEntity.Id);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, int>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Update(int id, MealUpdateModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return new Left<Error, bool>(new ArgumentNotSet(nameof(entity)));
                }

                using (var context = _factory.Create())
                {
                    var currentEntity = context.Meal.Include(a => a.FoodEntityMealEntities).FirstOrDefault(m => m.Id == id);
                    if (currentEntity == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound(nameof(entity)));
                    }

                    currentEntity.Price = entity.Price;

                    // this will remove old references, and after that new ones will be added
                    var addedEntities = entity.Foods.Select(foodId => new FoodEntityMealEntity { FoodEntityId = foodId, MealEntityId = id }).ToList();

                    var deletedEntities = currentEntity.FoodEntityMealEntities.Except(addedEntities).ToList();

                    deletedEntities.ForEach(d => currentEntity.FoodEntityMealEntities.Remove(d));

                    addedEntities.ForEach(a => currentEntity.FoodEntityMealEntities.Add(a));

                    currentEntity = context.Update(currentEntity).Entity;
                    context.SaveChanges();
                }

                return new Right<Error, bool>(true);
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Delete(int id)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var itemSet = context.Set<MealEntity>();
                    var item = itemSet.Find(id);
                    if (item == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound($"Record with Id='{id}' is not found."));
                    }

                    itemSet.Remove(item);
                    context.SaveChanges();
                    return new Right<Error, bool>(true);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }
    }
}
