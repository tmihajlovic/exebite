using Either;
using Exebite.Common;

namespace Exebite.DataAccess.Repositories
{
    public interface IDatabaseCommandRepository<TId, TInsert, TUpdate>
    {
        Either<Error, TId> Insert(TInsert entity);

        Either<Error, bool> Delete(TId id);

        Either<Error, bool> Update(TId id, TUpdate entity);
    }
}