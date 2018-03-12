using System.Linq;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class MealRepository : DatabaseRepository<Meal, MealEntity>, IMealRepository
    {
        private IFoodOrderingContextFactory _factory;

        public MealRepository(IFoodOrderingContextFactory factory)
            : base(factory)
        {
            this._factory = factory;
        }

        public override Meal Insert(Meal entity)
        {
            using (var context = _factory.Create())
            {
                var mealEntity = AutoMapperHelper.Instance.GetMappedValue<MealEntity>(entity, context);
                var resultEntity = context.Update(mealEntity);
                context.SaveChanges();
                var result = AutoMapperHelper.Instance.GetMappedValue<Meal>(resultEntity, context);
                return result;
            }
        }

        public override Meal Update(Meal entity)
        {
            using (var context = _factory.Create())
            {
                var mealEntity = AutoMapperHelper.Instance.GetMappedValue<MealEntity>(entity, context);
                context.Attach(mealEntity);
                context.SaveChanges();

                var resultEntity = context.Meals.FirstOrDefault(m => m.Id == entity.Id);
                var result = AutoMapperHelper.Instance.GetMappedValue<Meal>(resultEntity, context);

                return result;
            }
        }
    }
}
