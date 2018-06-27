using System;
using System.Collections.Generic;
using Either;

namespace Exebite.DataAccess.Repositories
{
    public interface IDatabaseQueryRepository<TResult, TQuery>
    {
        Either<Exception, (List<TResult>, int)> Query(TQuery queryModel);
    }
}
