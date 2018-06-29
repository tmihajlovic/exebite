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
    public class DailyMenuRepository : DatabaseRepository<DailyMenu, DailyMenuEntity, DailyMenuQueryModel>, IDailyMenuRepository
    {
        public DailyMenuRepository(IFoodOrderingContextFactory factory, IMapper mapper, ILogger<DailyMenuRepository> logger)
            : base(factory, mapper, logger)
        {
        }

        public override DailyMenu Insert(DailyMenu entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var dailyMenuEntity = new DailyMenuEntity
                {
                    Id = entity.Id,
                    RestaurantId = entity.RestaurantId,
                    Foods = entity.Foods.Select(food => context.Foods.Find(food.Id)).ToList()
                };

                var resultEntity = context.DailyMenues.Add(dailyMenuEntity).Entity;
                context.SaveChanges();
                return _mapper.Map<DailyMenu>(resultEntity);
            }
        }

        public override IList<DailyMenu> Query(DailyMenuQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new ArgumentNullException("queryModel can't be null");
            }

            using (var context = _factory.Create())
            {
                var query = context.DailyMenues.AsQueryable();

                if (queryModel.Id != null)
                {
                    query = query.Where(x => x.Id == queryModel.Id.Value);
                }

                if (queryModel.RestaurantId != null)
                {
                    query = query.Where(x => x.RestaurantId == queryModel.RestaurantId.Value);
                }

                var results = query.ToList();
                return _mapper.Map<IList<DailyMenu>>(results);
            }
        }

        public override DailyMenu Update(DailyMenu entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var currentEntity = context.DailyMenues.Find(entity.Id);
                currentEntity.RestaurantId = entity.RestaurantId;

                var addedEntities = entity.Foods.Select(food => context.Foods.Find(food.Id)).ToList();

                // this will remove old references, and after that new ones will be added
                currentEntity.Foods.Clear();

                addedEntities.ForEach(a => currentEntity.Foods.Add(a));

                context.SaveChanges();
                var resultEntity = context.DailyMenues.Include(a => a.Restaurant)
                                                      .Include(a => a.Foods)
                                                      .FirstOrDefault(l => l.Id == entity.Id);
                return _mapper.Map<DailyMenu>(resultEntity);
            }
        }
    }
}
