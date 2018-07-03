using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Microsoft.Extensions.Logging;

namespace Exebite.DataAccess.Repositories
{
    public abstract class DatabaseRepository<TModel, TEntity, QModel> : IDatabaseRepository<TModel, QModel>
        where TModel : class
        where TEntity : class
        where QModel : class
    {
#pragma warning disable SA1401 // Fields must be private
        protected readonly IMapper _mapper;
        protected readonly IFoodOrderingContextFactory _factory;
        protected readonly ILogger<IDatabaseRepository<TModel, QModel>> _logger;
#pragma warning restore SA1401 // Fields must be private

        protected DatabaseRepository(IFoodOrderingContextFactory factory, IMapper mapper, ILogger<IDatabaseRepository<TModel, QModel>> logger)
        {
            _factory = factory;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Insert new <see cref="TModel"/> to database
        /// </summary>
        /// <param name="entity">Object to be inserted</param>
        /// <returns>New <see cref="TModel"/> from database</returns>
        public abstract TModel Insert(TModel entity);

        public abstract TModel Update(TModel entity);

        public abstract IList<TModel> Query(QModel queryModel);

        public virtual IList<TModel> Get(int page, int size)
        {
            using (var dc = _factory.Create())
            {
                var list = dc.Set<TEntity>()
                             .Skip(page * size)
                             .Take(size)
                             .ToList();

                return _mapper.Map<IList<TModel>>(list);
            }
        }

        /// <summary>
        /// Get <see cref="TModel"/> by Id
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns><see cref="TModel"/> from database</returns>
        public TModel GetByID(int id)
        {
            using (var context = _factory.Create())
            {
                var itemSet = context.Set<TEntity>();
                var itemEntity = itemSet.Find(id);
                if (itemEntity != null)
                {
                    return _mapper.Map<TModel>(itemEntity);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Delete object from database
        /// </summary>
        /// <param name="id">Id of object to be deleted</param>
        public void Delete(int id)
        {
            using (var context = _factory.Create())
            {
                var itemSet = context.Set<TEntity>();
                var item = itemSet.Find(id);
                if (item != null)
                {
                    itemSet.Remove(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
