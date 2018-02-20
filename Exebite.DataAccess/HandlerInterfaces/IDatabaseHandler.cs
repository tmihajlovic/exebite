using System.Collections.Generic;

namespace Exebite.DataAccess
{
    public interface IDatabaseHandler<T>
    {
        IEnumerable<T> Get();
        T GetByID(int Id);
        void Insert(T entity);
        void Delete(int Id);
        void Update(T entity);
    }
}
