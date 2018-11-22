using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exebite.Common;

namespace WebClient.Services.Core
{
    public interface IRestService<TCreate, TQuery, TUpdate, TResult>
    {
        Task<int> CreateAsync(TCreate model);

        Task<PagingResult<TResult>> QueryAsync(TQuery query);

        Task DeleteByIdAsync(int id);

        Task<bool> UpdateAsync(int id, TUpdate model);
    }
}
