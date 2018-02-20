using System.Collections.Generic;
using System.Data.Entity;
using Exebite.DataAccess.Context;
using Exebite.Model;

namespace Exebite.DataAccess.Handlers
{
    public class MealHandler : IMealHandler
    {
        IFoodOrderingContextFactory _factory;
        public MealHandler(IFoodOrderingContextFactory factory)
        {
            this._factory = factory;
        }

        public void Delete(int Id)
        {
            using (var context = _factory.Create())
            {
                var meal = context.Meals.Find(Id);
                context.Meals.Remove(meal);
                context.SaveChanges();
            }
        }

        public IEnumerable<Meal> Get()
        {
            using (var context = _factory.Create())
            {
                var mealEnteties = new List<Meal>();

                foreach (var meal in context.Meals)
                {
                    var mealModel = AutoMapperHelper.Instance.GetMappedValue<Meal>(meal);
                    mealEnteties.Add(mealModel);
                }

                return mealEnteties;
            }
        }

        public Meal GetByID(int Id)
        {
            using (var context = _factory.Create())
            {
                var meal = context.Meals.Find(Id);
                var mealModel = AutoMapperHelper.Instance.GetMappedValue<Meal>(meal);

                return mealModel;
            }
        }

        public void Insert(Meal entity)
        {
            using (var context = _factory.Create())
            {
                var mealEntity = AutoMapperHelper.Instance.GetMappedValue<MealEntity>(entity);
                context.Meals.Add(mealEntity);
                context.SaveChanges();
            }
        }
        

        public void Update(Meal entity)
        {
            using (var context = _factory.Create())
            {
                var mealEntity = AutoMapperHelper.Instance.GetMappedValue<MealEntity>(entity);
                context.Entry(mealEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        

    }
}
