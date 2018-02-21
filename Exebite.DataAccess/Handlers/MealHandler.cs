using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.Model;

namespace Exebite.DataAccess.Handlers
{
    public class MealHandler : DatabaseHandler<Meal, MealEntity>, IMealHandler
    {
        IFoodOrderingContextFactory _factory;
        public MealHandler(IFoodOrderingContextFactory factory)
            :base(factory)
        {
            this._factory = factory;
        }
        
        public override Meal Insert(Meal entity)
        {
            using (var context = _factory.Create())
            {
                var mealEntity = AutoMapperHelper.Instance.GetMappedValue<MealEntity>(entity);
                
                //bind food
                for(int i=0; i<mealEntity.Foods.Count; i++)
                {
                    mealEntity.Foods[i] = context.Foods.SingleOrDefault(f => f.Id == mealEntity.Foods[i].Id);
                }


                var resultEntity = context.Meals.Add(mealEntity);
                context.SaveChanges();
                var result = AutoMapperHelper.Instance.GetMappedValue<Meal>(resultEntity);
                return result;
            }
        }
        

        public override Meal Update(Meal entity)
        {
            using (var context = _factory.Create())
            {
                var mealEntity = AutoMapperHelper.Instance.GetMappedValue<MealEntity>(entity);
                var oldMealEntity = context.Meals.SingleOrDefault(m => m.Id == entity.Id);
                context.Entry(oldMealEntity).CurrentValues.SetValues(mealEntity);

                //bind food

                for(int i =0; i< oldMealEntity.Foods.Count; i++)
                {
                    oldMealEntity.Foods[i] = context.Foods.SingleOrDefault(f => f.Id == oldMealEntity.Foods[i].Id);
                }

                context.SaveChanges();

                var resultEntity = context.Meals.FirstOrDefault(m => m.Id == entity.Id);
                var result = AutoMapperHelper.Instance.GetMappedValue<Meal>(resultEntity);

                return result;
            }
        }
        

    }
}
