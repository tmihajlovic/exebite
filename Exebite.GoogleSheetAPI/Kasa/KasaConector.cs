using System.Collections.Generic;
using System.Linq;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.Kasa
{
    public class KasaConector : IKasaConector
    {
        private readonly string _sheetId;
        private readonly string _range = "Kasa";
        private readonly SheetsService _googleSS;

        public KasaConector(IGoogleSheetServiceFactory googleSheetServiceFactory, IGoogleSpreadsheetIdFactory googleSpreadsheetIdFactory)
        {
            _googleSS = googleSheetServiceFactory.GetService();
            _sheetId = googleSpreadsheetIdFactory.GetSheetId(Enums.ESheetOwner.KASA);
        }

        public List<Customer> GetCustomersFromKasa()
        {
            List<Customer> customerList = new List<Customer>();

            SpreadsheetsResource.ValuesResource.GetRequest request =
                        _googleSS.Spreadsheets.Values.Get(_sheetId, _range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange sheetData = request.Execute();

            var names = sheetData.Values[0].Skip(1); // Take 1st column, 2nd row and on for names
            foreach (var name in names)
            {
                var newCustomer = new Customer { Name = name.ToString() };
                if (newCustomer.Name.EndsWith("JD"))
                {
                    newCustomer.Location = new Location { Name = "JD" };
                }
                else
                {
                    newCustomer.Location = new Location { Name = "Bulevar" };
                }

                customerList.Add(newCustomer);
            }

            return customerList;
        }
    }
}
