using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;

namespace Exebite.DataAccess.Repositories
{
    public class FoodCommandRepository : IFoodCommandRepository
    {
        private readonly IMapper _mapper;
        private readonly IFoodOrderingContextFactory _factory;

        public FoodCommandRepository(IFoodOrderingContextFactory factory, IMapper mapper)
        {
            _mapper = mapper;
            _factory = factory;
        }

        public Either<Error, int> Insert(FoodInsertModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return new Left<Error, int>(new ArgumentNotSet(nameof(entity)));
                }

                using (var context = _factory.Create())
                {
                    var foodEntity = new FoodEntity()
                    {
                        Name = entity.Name,
                        Type = entity.Type,
                        Price = entity.Price,
                        Description = entity.Description,
                        IsInactive = entity.IsInactive,
                        DailyMenuId = entity.DailyMenuId,
                        RestaurantId = entity.RestaurantId,
                    };

                    var addedEntity = context.Food.Add(foodEntity).Entity;
                    context.SaveChanges();
                    return new Right<Error, int>(addedEntity.Id);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, int>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Update(int id, FoodUpdateModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return new Left<Error, bool>(new ArgumentNotSet(nameof(entity)));
                }

                using (var context = _factory.Create())
                {
                    var currentEntity = context.Food.Find(id);
                    if (currentEntity == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound(nameof(entity)));
                    }

                    currentEntity.DailyMenuId = entity.DailyMenuId;
                    currentEntity.Description = entity.Description;
                    currentEntity.IsInactive = entity.IsInactive;
                    currentEntity.Name = entity.Name;
                    currentEntity.Price = entity.Price;
                    currentEntity.RestaurantId = entity.RestaurantId;
                    currentEntity.Type = entity.Type;
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
                    var itemSet = context.Set<FoodEntity>();
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

        public Either<Error, bool> DeactivatFoods(IList<int> foodIds)
        {
            try
            {
                using (var dc = _factory.Create())
                {
                    var itemSet = dc.Food.Where(x => foodIds.Contains(x.Id));
                    foreach (var item in itemSet)
                    {
                        item.IsInactive = true;
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
    }
}
