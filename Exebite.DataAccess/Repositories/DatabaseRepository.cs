using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Migrations;

namespace Exebite.DataAccess.Repositories
{
    public abstract class DatabaseRepository<TModel, TEntity> : IDatabaseRepository<TModel>
        where TModel : class
        where TEntity : class
    {
        private protected readonly IMapper _mapper;

        private protected readonly IFoodOrderingContextFactory _factory;

        protected DatabaseRepository(IFoodOrderingContextFactory factory, global::AutoMapper.IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        /// <summary>
        /// Insert new <see cref="TModel"/> to database
        /// </summary>
        /// <param name="entity">Object to be inserted</param>
        /// <returns>New <see cref="TModel"/> from database</returns>
        public abstract TModel Insert(TModel entity);

        public abstract TModel Update(TModel entity);

        public virtual IList<TModel> GetAll()
        {
            using (var dc = _factory.Create())
            {
                var items = dc
                    .Set<TEntity>()
                    .ToList();
                return items
                    .Select(x => _mapper.Map<TModel>(x))
                    .ToList();
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
                    var item = _mapper.Map<TModel>(itemEntity);
                    return item;
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
