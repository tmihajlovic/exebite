using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Extensions;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.SheetExtractor;

namespace Exebite.GoogleSheetAPI.Connectors.Kasa
{
    public class KasaConnector : IKasaConnector
    {
        private readonly IGoogleSheetExtractor _googleSheetExtractor;
        private readonly string _sheetId;
        private readonly string _range;

        private readonly ILocationQueryRepository _locationQueryRepository;

        public KasaConnector(
            IGoogleSheetExtractor googleSheetExtractor,
            IGoogleSpreadsheetIdFactory googleSheetIdFactory,
            ILocationQueryRepository locationQueryRepository)
        {
            _googleSheetExtractor = googleSheetExtractor;
            _sheetId = googleSheetIdFactory.GetSheetId(Enums.ESheetOwner.KASA);
            _range = "Kasa!A2:D";

            _locationQueryRepository = locationQueryRepository;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var locations = _locationQueryRepository
               .Query(new LocationQueryModel())
               .Map(x => x.Items)
               .Reduce(x => new List<Location>());

            return _googleSheetExtractor
                 .GetRows(_sheetId, _range)
                 .Values
                 .Select(col => new Customer()
                 {
                     Name = col.ExtractCell(0, "Missing Name"),
                     RoleId = 2,
                     LocationId = locations.FirstOrDefault(l => l.Name.Equals(col.ExtractCell(0, "Missing Name").MapLocationName()))?.Id ?? 0,
                     GoogleUserId = col.ExtractCell(1, string.Empty),
                     Balance = col.ExtractCell(3, 0m),
                 })
                 .Where(c => !string.IsNullOrWhiteSpace(c.GoogleUserId))
                 .OrderBy(c => c.GoogleUserId);
        }
    }
}