using Exebite.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.DataAccess.Handlers
{
    public class DatabaseHandler<T> : IDatabaseHandler<T> where T: class
    {
        IFoodOrderingContextFactory _factory;
        
        public DatabaseHandler(IFoodOrderingContextFactory factory)
        {
            _factory = factory;
        }

        
        public IEnumerable<T> Get()
        {
            using (var context = _factory.Create())
            {
                List<T> itemList = new List<T>();
                var entity = AutoMapperHelper.Instance.GetMappedValue<T>((T)null);
                var ET = typeof(T).MakeGenericType(entity.GetType());
                IQueryable<T> itemSet = context.Set<T>().AsQueryable();
                foreach(var item in itemSet)
                {
                    var itemModel = AutoMapperHelper.Instance.GetMappedValue<T>(item);
                    itemList.Add(itemModel);
                }
                return itemList;
            }
        }

        public T GetByID(int Id)
        {
            using (var context = _factory.Create())
            {
                var itemSet = context.Set<T>();
                var itemEntity = itemSet.Find(Id);
                var item = AutoMapperHelper.Instance.GetMappedValue<T>(itemEntity);
                return item;
            }
            throw new NotImplementedException();
        }

        public virtual void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            using (var context = _factory.Create())
            {
                var itemSet = context.Set<T>();
                var itemEntity = itemSet.Find(Id);
                itemSet.Remove(itemEntity);
                context.SaveChanges();
            }

        }
    }
}
