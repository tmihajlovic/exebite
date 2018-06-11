using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class FoodRepository : DatabaseRepository<Food, FoodEntity>, IFoodRepository
    {


        public FoodRepository(IFoodOrderingContextFactory factory, IMapper mapper)
            : base(factory, mapper)
        {
        }

        public override IList<Food> GetAll()
        {
            using (var context = _factory.Create())
            {
                return context.Foods.Select(_mapper.Map<Food>).ToList();
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
                    var foodModel = _mapper.Map<Food>(food);
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
                var foodEntity = _mapper.Map<FoodEntity>(entity);
                var resultEntity = context.Foods.Update(foodEntity).Entity;
                context.SaveChanges();
                return _mapper.Map<Food>(resultEntity);
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
                var foodEntity = _mapper.Map<FoodEntity>(entity);
                context.Update(foodEntity);
                context.SaveChanges();
                var resultEntity = context.Foods.FirstOrDefault(f => f.Id == foodEntity.Id);
                return _mapper.Map<Food>(resultEntity);
            }
        }
    }
}
