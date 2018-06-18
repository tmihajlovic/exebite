using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public class MealRepository : DatabaseRepository<Meal, MealEntity, MealQueryModel>, IMealRepository
    {
        public MealRepository(IFoodOrderingContextFactory factory, IMapper mapper)
            : base(factory, mapper)
        {
        }

        public override Meal Insert(Meal entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var locEntity = _mapper.Map<MealEntity>(entity);
                var resultEntity = context.Meals.Update(locEntity).Entity;
                context.SaveChanges();
                return _mapper.Map<Meal>(resultEntity);
            }
        }

        public override IList<Meal> Query(MealQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new System.ArgumentException("queryModel can't be null");
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
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var locationEntity = _mapper.Map<MealEntity>(entity);
                context.Update(locationEntity);
                context.SaveChanges();
                var resultEntity = context.Meals.FirstOrDefault(l => l.Id == entity.Id);
                return _mapper.Map<Meal>(resultEntity);
            }
        }
    }
}
