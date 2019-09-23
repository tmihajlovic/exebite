using System;
using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.Services
{
    /// <inheritdoc/>
    public sealed class GoogleSheetDataAccessService : IGoogleSheetDataAccessService
    {
        private readonly IEitherMapper _mapper;

        private readonly ICustomerCommandRepository _customerCommandRepository;
        private readonly ICustomerQueryRepository _customerQueryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleSheetDataAccessService"/> class.
        /// </summary>
        public GoogleSheetDataAccessService(
            IEitherMapper mapper,
            ICustomerCommandRepository customerCommandRepository,
            ICustomerQueryRepository customerQueryRepository)
        {
            _mapper = mapper;

            _customerCommandRepository = customerCommandRepository;
            _customerQueryRepository = customerQueryRepository;
        }

        /// <inheritdoc/>
        public Either<Error, (int Added, int Updated)> UpdateCustomers(IEnumerable<Customer> customers)
        {
            var added = 0;
            var updated = 0;

            if (!customers.Any())
            {
                return new Left<Error, (int, int)>(new ArgumentNotSet(nameof(customers)));
            }

            foreach (var customer in customers)
            {
                var exists = _customerQueryRepository
                     .ExistsByGoogleId(customer.GoogleUserId)
                     .Reduce(r => false, ex => Console.WriteLine(ex.ToString()));

                if (!exists)
                {
                    added += _mapper
                        .Map<CustomerInsertModel>(customer)
                        .Map(_customerCommandRepository.Insert)
                        .Reduce(r => 0, ex => Console.WriteLine(ex.ToString())) > 0 ? 1 : 0;
                }
                else
                {
                    updated += _mapper
                        .Map<CustomerUpdateModel>(customer)
                        .Map(_customerCommandRepository.UpdateByGoogleId)
                        .Reduce(r => false, ex => Console.WriteLine(ex.ToString())) ? 1 : 0;
                }
            }

            return new Right<Error, (int, int)>((added, updated));
        }
    }
}