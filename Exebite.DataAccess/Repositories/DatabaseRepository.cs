using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Migrations;

namespace Exebite.DataAccess.Repositories
{
    public abstract class DatabaseRepository<TModel, TEntity> : IDatabaseRepository<TModel>
        where TModel : class
        where TEntity : class
    {
        private IFoodOrderingContextFactory _factory;

        protected DatabaseRepository(IFoodOrderingContextFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Insert new <see cref="TModel"/> to database
        /// </summary>
        /// <param name="entity">Object to be inserted</param>
        /// <returns>New <see cref="TModel"/> from database</returns>
        public abstract TModel Insert(TModel entity);

        public abstract TModel Update(TModel entity);

        public virtual IEnumerable<TModel> GetAll()
        {
            using (var context = _factory.Create())
            {
                List<TModel> itemList = new List<TModel>();
                IQueryable<TEntity> itemSet = context.Set<TEntity>().AsQueryable();

                foreach (var item in itemSet)
                {
                    var itemModel = AutoMapperHelper.Instance.GetMappedValue<TModel>(item, context);
                    itemList.Add(itemModel);
                }

                return itemList;
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

            throw new NotImplementedException();
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
                itemSet.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
