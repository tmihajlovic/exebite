using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class FoodRepository : DatabaseRepository<Food, FoodEntity>, IFoodRepository
    {
        private IFoodOrderingContextFactory _factory;

        public FoodRepository(IFoodOrderingContextFactory factory)
            : base(factory)
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
                var resultEntity = context.Foods.Update(foodEntity).Entity;
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
                foodEntity.FoodEntityMealEntity = new List<FoodEntityMealEntities>();
                foodEntity.FoodEntityMealEntity.AddRange(context.FoodEntityMealEntities.Where(f => f.FoodEntityId == foodEntity.Id));
                var dbFood = context.Foods.Find(entity.Id);
                context.Entry(dbFood).CurrentValues.SetValues(foodEntity);
                dbFood.FoodEntityMealEntity.Clear();
                dbFood.FoodEntityMealEntity.AddRange(foodEntity.FoodEntityMealEntity);
                context.SaveChanges();
                var resultEntity = context.Foods.FirstOrDefault(f => f.Id == foodEntity.Id);
                var result = AutoMapperHelper.Instance.GetMappedValue<Food>(resultEntity);
                return result;
            }
        }
    }
}
