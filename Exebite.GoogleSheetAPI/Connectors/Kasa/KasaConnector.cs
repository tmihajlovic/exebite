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
    public sealed class KasaConnector : IKasaConnector
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
                 .Select(col =>
                 {
                     var name = _googleSheetExtractor.ExtractCell(col, 0, "MissingName");
                     var locationName = name.EndsWith("MM")
                        ? "Execom MM"
                        : "Execom VS";

                     return new Customer()
                     {
                         Name = name,
                         RoleId = 2,
                         LocationId = locations.FirstOrDefault(l => l.Name.Equals(locationName))?.Id ?? 0,
                         GoogleUserId = _googleSheetExtractor.ExtractCell(col, 1, string.Empty),
                         Balance = _googleSheetExtractor.ExtractCell(col, 3, 0m),
                     };
                 })
                 .Where(c => !string.IsNullOrWhiteSpace(c.GoogleUserId) && !c.Name.TrimmedAndLowercasedEqualsTo("gost"))
                 .OrderBy(c => c.GoogleUserId);
        }
    }
}