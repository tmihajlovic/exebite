using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Exebite.DataAccess.Repositories
{
    public class FoodRepository : DatabaseRepository<Food, FoodEntity, FoodQueryModel>, IFoodRepository
    {
        public FoodRepository(IFoodOrderingContextFactory factory, IMapper mapper, ILogger<FoodRepository> logger)
            : base(factory, mapper, logger)
        {
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
                var foodEntity = new FoodEntity
                {
                    Description = entity.Description,
                    IsInactive = entity.IsInactive,
                    Name = entity.Name,
                    Price = entity.Price,
                    Type = entity.Type,
                    RestaurantId = entity.RestaurantId,
                };

                var createdEntry = context.Foods.Add(foodEntity).Entity;
                context.SaveChanges();
                createdEntry = context.Foods.Include(a => a.Restaurant)
                                                .ThenInclude(a => a.DailyMenu)
                                            .FirstOrDefault(a => a.Id == createdEntry.Id);
                return _mapper.Map<Food>(createdEntry);
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
                var currentEntity = context.Foods.Find(entity.Id);
                currentEntity.Description = entity.Description;
                currentEntity.RestaurantId = entity.RestaurantId;
                currentEntity.IsInactive = entity.IsInactive;
                currentEntity.Name = entity.Name;
                currentEntity.Price = entity.Price;
                currentEntity.Type = entity.Type;

                context.SaveChanges();
                return _mapper.Map<Food>(currentEntity);
            }
        }

        public override IList<Food> Query(FoodQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new System.ArgumentException("queryModel can't be null");
            }

            using (var context = _factory.Create())
            {
                var query = context.Foods.AsQueryable();

                if (queryModel.Id != null)
                {
                    query = query.Where(x => x.Id == queryModel.Id.Value);
                }

                var results = query.ToList();
                return _mapper.Map<IList<Food>>(results);
            }
        }
    }
}
