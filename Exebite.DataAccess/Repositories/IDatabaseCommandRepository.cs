using Either;
using System;

namespace Exebite.DataAccess.Repositories
{
    public interface IDatabaseCommandRepository<TID, IT, UT>
    {
        Either<Exception, TID> Insert(IT entity);

        Either<Exception, bool> Delete(TID id);

        Either<Exception, bool> Update(TID id, UT entity);
    }
}