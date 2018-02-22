using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class FoodRepository : DatabaseRepository<Food,FoodEntity>, IFoodRepository
    {
        IFoodOrderingContextFactory _factory;
        public FoodRepository(IFoodOrderingContextFactory factory)
            :base(factory)
        {
            this._factory = factory;
        }

        public IEnumerable<Food> GetByRestaurant(Restaurant restaurant)
        {
            using (var context = _factory.Create())
            {
                var foodEntities = new List<Food>();

                foreach (var food in context.Foods.Where(f => f.Restaurant.Name == restaurant.Name))
                {
                    var foodModel = AutoMapperHelper.Instance.GetMappedValue<Food>(food);
                    foodEntities.Add(foodModel);
                }

                return foodEntities;
            }
        }

        public override Food Insert(Food entity)
        {
            using (var context = _factory.Create())
            {
                var foodEntity = AutoMapperHelper.Instance.GetMappedValue<FoodEntity>(entity);
                var resultEntity = context.Foods.Add(foodEntity);
                context.SaveChanges();
                var result = AutoMapperHelper.Instance.GetMappedValue<Food>(resultEntity);
                return result;
            }
        }
        

        public override Food Update(Food entity)
        {
            using (var context = _factory.Create())
            {
                var foodEntity = AutoMapperHelper.Instance.GetMappedValue<FoodEntity>(entity);
                var oldFoodEntity = context.Foods.SingleOrDefault(f => f.Id == entity.Id);
                context.Entry(oldFoodEntity).CurrentValues.SetValues(foodEntity);
                context.SaveChanges();
                var resultEntity = context.Foods.FirstOrDefault(f => f.Id == entity.Id);
                var result = AutoMapperHelper.Instance.GetMappedValue<Food>(resultEntity);
                return result;
            }
        }
        
    }
}
