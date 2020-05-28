using System;
using AutoMapper;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;

namespace Exebite.DataAccess.Repositories
{
    public class OrderCommandRepository : IOrderCommandRepository
    {
        private readonly IMapper _mapper;
        private readonly IMealOrderingContextFactory _factory;

        public OrderCommandRepository(IMealOrderingContextFactory factory, IMapper mapper)
        {
            _mapper = mapper;
            _factory = factory;
        }

        public Either<Error, long> Insert(OrderInsertModel entity)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var orderEntity = new OrderEntity()
                    {
                        CustomerId = entity.CustomerId,
                        Date = entity.Date,
                        Price = entity.Price,
                        LocationId = entity.LocationId
                    };

                    var addedEntity = context.Order.Add(orderEntity).Entity;
                    context.SaveChanges();

                    return new Right<Error, long>(addedEntity.Id);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, long>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Update(long id, OrderUpdateModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return new Left<Error, bool>(new ArgumentNotSet(nameof(entity)));
                }

                using (var context = _factory.Create())
                {
                    var currentEntity = context.Order.Find(id);
                    if (currentEntity == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound(nameof(entity)));
                    }

                    currentEntity.CustomerId = entity.CustomerId;
                    currentEntity.Date = entity.Date;
                    currentEntity.LocationId = entity.LocationId;
                    currentEntity.Price = entity.Price;

                    currentEntity = context.Update(currentEntity).Entity;
                    context.SaveChanges();
                }

                return new Right<Error, bool>(true);
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Delete(long id)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var itemSet = context.Set<OrderEntity>();
                    var item = itemSet.Find(id);

                    if (item == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound($"Record with Id='{id}' is not found."));
                    }

                    itemSet.Remove(item);
                    context.SaveChanges();

                    return new Right<Error, bool>(true);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }
    }
}
