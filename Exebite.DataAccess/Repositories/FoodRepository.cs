using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class FoodRepository : DatabaseRepository<Food, FoodEntity>, IFoodRepository
    {
        private readonly IFoodOrderingContextFactory _factory;

        public FoodRepository(IFoodOrderingContextFactory factory, IExebiteMapper mapper)
            : base(factory, mapper)
        {
            _factory = factory;
        }

        public override IList<Food> GetAll()
        {
            using (var context = _factory.Create())
            {
                var items = context.Foods.ToList();
                return items.Select(_exebiteMapper.Map<Food>).ToList();
            }
        }

        public IEnumerable<Food> GetByRestaurant(Restaurant restaurant)
        {
            if (restaurant == null)
            {
                throw new System.ArgumentNullException(nameof(restaurant));
            }

            using (var context = _factory.Create())
            {
                var foodEntities = new List<Food>();

                foreach (var food in context.Foods.Where(f => f.Restaurant.Name == restaurant.Name))
                {
                    var foodModel = AutoMapperHelper.Instance.GetMappedValue<Food>(food, context);
                    foodEntities.Add(foodModel);
                }

                return foodEntities;
            }
        }

        public override Food Insert(Food entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var foodEntity = AutoMapperHelper.Instance.GetMappedValue<FoodEntity>(entity, context);
                var resultEntity = context.Foods.Update(foodEntity).Entity;
                context.SaveChanges();
                return AutoMapperHelper.Instance.GetMappedValue<Food>(resultEntity, context);
            }
        }

        public override Food Update(Food entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var foodEntity = AutoMapperHelper.Instance.GetMappedValue<FoodEntity>(entity, context);
                context.Update(foodEntity);
                context.SaveChanges();
                var resultEntity = context.Foods.FirstOrDefault(f => f.Id == foodEntity.Id);
                return AutoMapperHelper.Instance.GetMappedValue<Food>(resultEntity, context);
            }
        }
    }
}
