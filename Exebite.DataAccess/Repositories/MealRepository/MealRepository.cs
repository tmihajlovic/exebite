using System;
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
    public class MealRepository : DatabaseRepository<Meal, MealEntity, MealQueryModel>, IMealRepository
    {
        public MealRepository(IFoodOrderingContextFactory factory, IMapper mapper, ILogger<MealRepository> logger)
            : base(factory, mapper, logger)
        {
        }

        public override Meal Insert(Meal entity)
        {
            _logger.LogDebug("Insert started.");
            if (entity == null)
            {
                _logger.LogError($"Argument {entity} is null");
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var mealEntity = new MealEntity
                {
                    Id = entity.Id,
                    Price = entity.Price,
                    FoodEntityMealEntities = entity.Foods.Select(x => new FoodEntityMealEntities { FoodEntityId = x.Id }).ToList()

                };
                var createEntity = context.Add(mealEntity).Entity;
                context.SaveChanges();
                _logger.LogDebug("Insert finished.");

                createEntity = context.Meals.Include(r => r.FoodEntityMealEntities)
                                            .FirstOrDefault(r => r.Id == createEntity.Id);

                return _mapper.Map<Meal>(createEntity);
            }
        }

        public override IList<Meal> Query(MealQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new ArgumentNullException("queryModel can't be null");
            }

            using (var context = _factory.Create())
            {
                var query = context.Meals.AsQueryable();

                if (queryModel.Id != null)
                {
                    query = query.Where(x => x.Id == queryModel.Id.Value);
                }

                var results = query.ToList();
                return _mapper.Map<IList<Meal>>(results);
            }
        }

        public override Meal Update(Meal entity)
        {
            _logger.LogDebug("Update started.");
            if (entity == null)
            {
                _logger.LogError($"Argument {entity} is null");
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var currentEntity = context.Meals.Find(entity.Id);
                currentEntity.Price = entity.Price;

                // this will remove old references, and after that new ones will be added
                var addedEntities = Enumerable.Range(0, entity.Foods.Count).Select(a =>
                {
                    return new FoodEntityMealEntities { FoodEntityId = entity.Foods[a].Id, MealEntityId = entity.Id };
                }).ToList();

                var deletedEntities = currentEntity.FoodEntityMealEntities.Except(addedEntities).ToList();

                deletedEntities.ForEach(d => currentEntity.FoodEntityMealEntities.Remove(d));

                addedEntities.ForEach(a => currentEntity.FoodEntityMealEntities.Add(a));

                currentEntity = context.Update(currentEntity).Entity;
                context.SaveChanges();
                _logger.LogDebug("Update finished.");

                currentEntity = context.Meals.Include(a => a.FoodEntityMealEntities)
                                             .FirstOrDefault(l => l.Id == entity.Id);
                return _mapper.Map<Meal>(currentEntity);
            }
        }
    }
}
