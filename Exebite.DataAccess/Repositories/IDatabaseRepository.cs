using System.Collections.Generic;

namespace Exebite.DataAccess.Repositories
{
    public interface IDatabaseRepository<T>
    {
        /// <summary>
        /// Gets all entities from database
        /// </summary>
        /// <returns>IEnumerable of all entities</returns>
        IList<T> GetAll();

        /// <summary>
        /// Get item by Id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>Item</returns>
        T GetByID(int id);

        /// <summary>
        /// Insert new item in database
        /// </summary>
        /// <param name="entity">New item</param>
        /// <returns>New item from database</returns>
        T Insert(T entity);

        /// <summary>
        /// Delete item from database
        /// </summary>
        /// <param name="id">Id of item</param>
        void Delete(int id);

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="entity">Updated item</param>
        /// <returns>Updated item from database</returns>
        T Update(T entity);
    }
}
