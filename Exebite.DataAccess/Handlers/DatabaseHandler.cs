using Exebite.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.DataAccess.Handlers
{
    public class DatabaseHandler<TModel, TEntity> : IDatabaseHandler<TModel> where TModel: class where TEntity : class
    {
        IFoodOrderingContextFactory _factory;
        
        public DatabaseHandler(IFoodOrderingContextFactory factory)
        {
            _factory = factory;
        }

        
        public IEnumerable<TModel> Get()
        {
            using (var context = _factory.Create())
            {
                List<TModel> itemList = new List<TModel>();
                var entity = AutoMapperHelper.Instance.GetMappedValue<TEntity>((TModel)null);
                var ET = typeof(TModel).MakeGenericType(entity.GetType());
                IQueryable<TModel> itemSet = context.Set<TModel>().AsQueryable();
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
                var itemSet = context.Set<TModel>();
                var itemEntity = itemSet.Find(Id);
                var item = AutoMapperHelper.Instance.GetMappedValue<TModel>(itemEntity);
                return item;
            }
            throw new NotImplementedException();
        }

        public virtual void Insert(TModel entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            using (var context = _factory.Create())
            {
                var itemSet = context.Set<TModel>();
                var itemEntity = itemSet.Find(Id);
                itemSet.Remove(itemEntity);
                context.SaveChanges();
            }

        }
    }
}
