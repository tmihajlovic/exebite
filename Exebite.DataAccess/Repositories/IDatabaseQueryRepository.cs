using Either;
using System;
using System.Collections.Generic;

namespace Exebite.DataAccess.Repositories
{
    public interface IDatabaseQueryRepository<T, Q>
    {
        Either<Exception,(List<T>, int) > Query(Q queryModel);
    }
}
