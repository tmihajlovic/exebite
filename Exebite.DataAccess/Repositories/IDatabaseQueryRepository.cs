using Either;

namespace Exebite.DataAccess.Repositories
{
    public interface IDatabaseQueryRepository<TResult, TQuery>
    {
        Either<Error, PagingResult<TResult>> Query(TQuery queryModel);
    }
}
