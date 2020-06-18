using System;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;

namespace Exebite.DataAccess.Repositories
{
    public class DailyMenuCommandRepository : IDailyMenuCommandRepository
    {
        private readonly IMealOrderingContextFactory _factory;

        public DailyMenuCommandRepository(IMealOrderingContextFactory factory)
        {
            _factory = factory;
        }

        public Either<Error, long> Insert(DailyMenuInsertModel entity)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var dailyMenuEntity = new DailyMenuEntity
                    {
                        RestaurantId = entity.RestaurantId,
                        Date = entity.Date,
                        Note = entity.Note,
                    };

                    var addedEntity = context.DailyMenu.Add(dailyMenuEntity).Entity;

                    if (entity.Meals != null)
                    {
                        foreach (var meal in entity.Meals)
                        {
                            addedEntity.DailyMenuToMeals.Add(new DailyMenuToMealEntity() { DailyMenuId = addedEntity.Id, MealId = meal.Id });
                        }
                    }

                    context.SaveChanges();
                    return new Right<Error, long>(addedEntity.Id);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, long>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Update(long id, DailyMenuUpdateModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return new Left<Error, bool>(new ArgumentNotSet(nameof(entity)));
                }

                using (var context = _factory.Create())
                {
                    var currentEntity = context.DailyMenu.Find(id);
                    if (currentEntity == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound(nameof(entity)));
                    }

                    currentEntity.RestaurantId = entity.RestaurantId;
                    currentEntity.Date = entity.Date;
                    currentEntity.Note = entity.Note;

                    if (entity.Meals != null)
                    {
                        foreach (var meal in entity.Meals)
                        {
                            currentEntity.DailyMenuToMeals.Add(new DailyMenuToMealEntity() { DailyMenuId = currentEntity.Id, MealId = meal.Id });
                        }
                    }

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
                    var itemSet = context.Set<DailyMenuEntity>();
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
