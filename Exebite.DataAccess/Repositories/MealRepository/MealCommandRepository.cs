using System;
using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;

namespace Exebite.DataAccess.Repositories
{
    public class MealCommandRepository : IMealCommandRepository
    {
        private readonly IMealOrderingContextFactory _factory;

        public MealCommandRepository(IMealOrderingContextFactory factory)
        {
            _factory = factory;
        }

        public Either<Error, long> Insert(MealInsertModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return new Left<Error, long>(new ArgumentNotSet(nameof(entity)));
                }

                using (var context = _factory.Create())
                {
                    var mealEntity = new MealEntity()
                    {
                        Name = entity.Name,
                        Type = (int)entity.Type,
                        Price = entity.Price,
                        Description = entity.Description,
                        Note = entity.Note,
                        IsActive = entity.IsActive,
                        RestaurantId = entity.RestaurantId,
                    };

                    var addedEntity = context.Meal.Add(mealEntity).Entity;
                    context.SaveChanges();

                    return new Right<Error, long>(addedEntity.Id);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, long>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Update(long id, MealUpdateModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return new Left<Error, bool>(new ArgumentNotSet(nameof(entity)));
                }

                using (var context = _factory.Create())
                {
                    var currentEntity = context.Meal.Find(id);

                    if (currentEntity == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound(nameof(entity)));
                    }

                    currentEntity.Description = entity.Description;
                    currentEntity.IsActive = entity.IsActive;
                    currentEntity.Name = entity.Name;
                    currentEntity.Price = entity.Price;
                    currentEntity.RestaurantId = entity.RestaurantId;
                    currentEntity.Type = (int)entity.Type;
                    currentEntity.Note = entity.Note;

                    context.SaveChanges();
                }

                return new Right<Error, bool>(true);
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Delete(long id)
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

        public Either<Error, bool> DeactivateMeals(IList<long> ids)
        {
            try
            {
                using (var dc = _factory.Create())
                {
                    var itemSet = dc.Meal.Where(x => ids.Contains(x.Id));

                    foreach (var item in itemSet)
                    {
                        item.IsActive = false;
                    }

                    dc.SaveChanges();

                    return new Right<Error, bool>(true);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> UpdateByNameAndRestaurantId(MealUpdateModel meal)
        {
            try
            {
                if (meal == null)
                {
                    return new Left<Error, bool>(new RecordNotFound(nameof(meal)));
                }

                using (var context = _factory.Create())
                {
                    var dbFood = context.Meal.FirstOrDefault(f =>
                        f.Name.Equals(meal.Name, StringComparison.OrdinalIgnoreCase) &&
                        f.RestaurantId == meal.RestaurantId);

                    if (dbFood == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound(nameof(dbFood)));
                    }

                    dbFood.Note = meal.Note;
                    dbFood.Description = meal.Description;
                    dbFood.IsActive = meal.IsActive;
                    dbFood.Name = meal.Name;
                    dbFood.Price = meal.Price;
                    dbFood.RestaurantId = meal.RestaurantId;
                    dbFood.Type = (int)meal.Type;

                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }
    }
}
