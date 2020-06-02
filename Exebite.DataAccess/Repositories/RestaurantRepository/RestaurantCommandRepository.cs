using System;
using AutoMapper;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;

namespace Exebite.DataAccess.Repositories
{
    public class RestaurantCommandRepository : IRestaurantCommandRepository
    {
        private readonly IMapper _mapper;
        private readonly IMealOrderingContextFactory _factory;

        public RestaurantCommandRepository(IMealOrderingContextFactory factory, IMapper mapper)
        {
            _mapper = mapper;
            _factory = factory;
        }

        public Either<Error, long> Insert(RestaurantInsertModel entity)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var restaurantEntity = new RestaurantEntity()
                    {
                        Name = entity.Name,
                        IsActive = entity.IsActive,
                        LogoUrl = entity.LogoUrl,
                        OrderDue = entity.OrderDue,
                        SheetId = entity.SheetId,
                        Contact = entity.Contact
                    };

                    var addedEntity = context.Restaurant.Add(restaurantEntity).Entity;
                    context.SaveChanges();
                    return new Right<Error, long>(addedEntity.Id);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, long>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Update(long id, RestaurantUpdateModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return new Left<Error, bool>(new ArgumentNotSet(nameof(entity)));
                }

                using (var context = _factory.Create())
                {
                    var currentEntity = context.Restaurant.Find(id);
                    if (currentEntity == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound(nameof(entity)));
                    }

                    currentEntity.Name = entity.Name;
                    currentEntity.IsActive = entity.IsActive;
                    currentEntity.LogoUrl = entity.LogoUrl;
                    currentEntity.OrderDue = entity.OrderDue;
                    currentEntity.SheetId = entity.SheetId;
                    currentEntity.Contact = entity.Contact;

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
                    var itemSet = context.Set<RestaurantEntity>();
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
