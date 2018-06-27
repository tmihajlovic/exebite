using System;
using Either;

namespace Exebite.DataAccess.Repositories
{
    public interface IDatabaseCommandRepository<TId, TInsert, TUpdate>
    {
        Either<Exception, TId> Insert(TInsert entity);

        Either<Exception, bool> Delete(TId id);

        Either<Exception, bool> Update(TId id, TUpdate entity);
    }
}