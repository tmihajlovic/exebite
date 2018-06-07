using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Migrations;

namespace Exebite.DataAccess.Repositories
{
    public abstract class DatabaseRepository<TModel, TEntity> : IDatabaseRepository<TModel>
        where TModel : class
        where TEntity : class
    {
        private readonly IFoodOrderingContextFactory _factory;
        protected readonly IExebiteMapper _exebiteMapper;

        protected DatabaseRepository(IFoodOrderingContextFactory factory, IExebiteMapper exebiteMapper)
        {
            _factory = factory;
            _exebiteMapper = exebiteMapper;
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
            using (var context = _factory.Create())
            {
                //List<TModel> itemList = new List<TModel>();
                var items = context
                                .Set<TEntity>()
                                //.AsQueryable()
                                .ProjectTo<TModel>(_exebiteMapper.ConfigurationProvider).ToList();

                //foreach (var item in itemSet)
                //{
                //    var itemModel = AutoMapperHelper.Instance.GetMappedValue<TModel>(item, context);
                //    itemList.Add(itemModel);
                //}

                return items;
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
                    var item = AutoMapperHelper.Instance.GetMappedValue<TModel>(itemEntity, context);
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
