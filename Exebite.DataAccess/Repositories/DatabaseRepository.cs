using Exebite.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exebite.DataAccess.Repositories
{
    public abstract class DatabaseRepository<TModel, TEntity> : IDatabaseRepository<TModel> where TModel: class where TEntity : class
    {
        IFoodOrderingContextFactory _factory;
        
        public DatabaseRepository(IFoodOrderingContextFactory factory)
        {
            _factory = factory;
        }

        public abstract TModel Insert(TModel entity);

        public abstract TModel Update(TModel entity);
        
        public IEnumerable<TModel> GetAll()
        {
            using (var context = _factory.Create())
            {
                List<TModel> itemList = new List<TModel>();
                IQueryable<TEntity> itemSet = context.Set<TEntity>().AsQueryable();
                foreach(var item in itemSet)
                {
                    var itemModel = AutoMapperHelper.Instance.GetMappedValue<TModel>(item);
                    itemList.Add(itemModel);
                }
                return itemList;
            }
        }

        public TModel GetByID(int Id)
        {
            using (var context = _factory.Create())
            {
                var itemSet = context.Set<TEntity>();
                var itemEntity = itemSet.Find(Id);
                var item = AutoMapperHelper.Instance.GetMappedValue<TModel>(itemEntity);
                return item;
            }
            throw new NotImplementedException();
        }
        
        public void Delete(int Id)
        {
            using (var context = _factory.Create())
            {
                var itemSet = context.Set<TEntity>();
                var item = itemSet.Find(Id);
                var itemEntity = AutoMapperHelper.Instance.GetMappedValue<TEntity>(item);
                itemSet.Remove(item);
                context.SaveChanges();
            }

        }
    }
}
