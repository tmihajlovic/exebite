﻿using System;
using System.Collections.Generic;
using Either;
using Exebite.Common;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Connectors.Kasa;
using Exebite.GoogleSheetAPI.Connectors.Restaurants;

namespace Exebite.GoogleSheetAPI.Services
{
    /// <inheritdoc/>
    public sealed class GoogleSheetAPIService : IGoogleSheetAPIService
    {
        private readonly IEitherMapper _mapper;
        private readonly IGoogleSheetDataAccessService _googleSheetDataAccessService;

        private readonly IKasaConnector _kasaConnector;
        private readonly ILipaConnector _lipaConnector;
        private readonly ITopliObrokConnector _topliObrokConnector;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleSheetAPIService"/> class.
        /// </summary>
        public GoogleSheetAPIService(
            IEitherMapper mapper,
            IGoogleSheetDataAccessService googleSheetDataAccessService,
            IKasaConnector kasaConnector,
            ILipaConnector lipaConnector,
            ITopliObrokConnector topliObrokConnector)
        {
            _mapper = mapper;
            _googleSheetDataAccessService = googleSheetDataAccessService;
            _kasaConnector = kasaConnector;
            _lipaConnector = lipaConnector;
            _topliObrokConnector = topliObrokConnector;
        }

        /// <inheritdoc/>
        public void UpdateCustomers()
        {
            _mapper
               .Map<IEnumerable<Customer>>(_kasaConnector.GetAllCustomers())
               .Map(_googleSheetDataAccessService.UpdateCustomers)
               .Map(count => LogRowsAffected(count, typeof(Customer), nameof(_kasaConnector)))
               .Reduce(_ => (0, 0), ex => Console.WriteLine(ex.ToString()));
        }

        /// <inheritdoc/>
        public void UpdateDailyMenuLipa()
        {
            _mapper
                .Map<IEnumerable<Food>>(_lipaConnector.GetDailyMenu())
                .Map(_googleSheetDataAccessService.UpdateFoods)
                .Map(count => LogRowsAffected(count, typeof(Food), nameof(_lipaConnector)))
                .Reduce(_ => (0, 0), ex => Console.WriteLine(ex.ToString()));
        }

        /// <inheritdoc/>
        public void UpdateDailyMenuTopliObrok()
        {
            _mapper
                .Map<IEnumerable<Food>>(_topliObrokConnector.GetDailyMenu())
                .Map(_googleSheetDataAccessService.UpdateFoods)
                .Map(count => LogRowsAffected(count, typeof(Food), nameof(_topliObrokConnector)))
                .Reduce(_ => (0, 0), ex => Console.WriteLine(ex.ToString()));
        }
        /// <summary>
        /// Output the result of the operation to the console.
        /// <para>Used for debugging purposes.</para>
        /// </summary>
        /// <param name="rows">Number of rows Added/Updated.</param>
        /// <param name="modelType">Type of the DomainModel.</param>
        /// <param name="connectorName">Name of the connector used by the domain.</param>
        /// <returns>Resulting number of rows Added/Updated.</returns>
        private (int, int) LogRowsAffected((int added, int updated) rows, Type modelType, string connectorName)
        {
            Console.WriteLine(
                "\nInserted  >> {0} records \nUpdated   >> {1} records \nType      >> {2}\nConnector >> {3}",
                rows.added,
                rows.updated,
                modelType,
                connectorName);
            return rows;
        }
    }
}