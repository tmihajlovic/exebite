using System;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class MealRepository : DatabaseRepository<Meal, MealEntity>, IMealRepository
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
