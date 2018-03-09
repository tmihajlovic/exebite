using System.Collections.Generic;

namespace Exebite.DataAccess
{
    public interface IDatabaseRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetByID(int id);

        T Insert(T entity);

        void Delete(int id);

        T Update(T entity);
    }
}
