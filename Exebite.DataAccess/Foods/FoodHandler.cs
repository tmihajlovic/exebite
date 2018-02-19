using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.Model;

namespace Exebite.DataAccess.Foods
{
    public class FoodHandler : IFoodHandler
    {
        IFoodOrderingContextFactory _factory;
        public FoodHandler(IFoodOrderingContextFactory factory)
        {
            this._factory = factory;
        }

        public void Delete(int Id)
        {
            using (var context = _factory.Create())
            {
                var food = context.Foods.Find(Id);
                context.Foods.Remove(food);
                context.SaveChanges();
            }
        }

        public IEnumerable<Food> Get()
        {
            using (var context = _factory.Create())
            {
                var foodEntities = new List<Food>();

                foreach (var food in context.Foods)
                {
                    var foodModel = AutoMapperHelper.Instance.GetMappedValue<Food>(food);
                    foodEntities.Add(foodModel);
                }

                return foodEntities;
            }
        }

        public Food GetByID(int Id)
        {
            using (var context = _factory.Create())
            {
                var foodEntity = context.Foods.Find(Id);
                var foodModel = AutoMapperHelper.Instance.GetMappedValue<Food>(foodEntity);
                return foodModel;
            }
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

        public void Insert(Food entity)
        {
            using (var context = _factory.Create())
            {
                var foodEntity = AutoMapperHelper.Instance.GetMappedValue<FoodEntity>(entity);
                context.Foods.Add(foodEntity);
                context.SaveChanges();
            }
        }
        

        public void Update(Food entity)
        {
            using (var context = _factory.Create())
            {
                var foodEntity = AutoMapperHelper.Instance.GetMappedValue<FoodEntity>(entity);
                context.Entry(foodEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        
    }
}
