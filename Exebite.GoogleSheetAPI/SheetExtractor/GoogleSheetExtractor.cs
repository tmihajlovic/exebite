using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.SheetExtractor
{
    public class GoogleSheetExtractor : IGoogleSheetExtractor
    {
        private readonly SheetsService _sheetService;
        private readonly IGoogleSheetServiceFactory _googleSSFactory;

        public GoogleSheetExtractor(IGoogleSheetServiceFactory googleSheetServiceFactory)
        {
            _googleSSFactory = googleSheetServiceFactory;
            _sheetService = _googleSSFactory.GetService();
        }

        public ValueRange GetRows(string sheetId, string range)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request =
            _sheetService.Spreadsheets.Values.Get(sheetId, range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.ROWS;
            ValueRange result = request.Execute();

            return result;
        }

        public ValueRange GetColumns(string sheetId, string range)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request =
            _sheetService.Spreadsheets.Values.Get(sheetId, range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange result = request.Execute();

            return result;
        }

        public void Update(ValueRange body, string sheetId, string range)
        {
            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = _sheetService.Spreadsheets.Values.Update(body, sheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            updateRequest.Execute();
        }

        public void Clear(string sheetId, string range)
        {
            ClearValuesRequest body = new ClearValuesRequest();
            SpreadsheetsResource.ValuesResource.ClearRequest clearRequest = _sheetService.Spreadsheets.Values.Clear(body, sheetId, range);
            clearRequest.Execute();
        }
    }
}
