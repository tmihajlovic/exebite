using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.Test.Mocks
{
    public class GoogleSheetServiceFake_ReturnEmpty : IGoogleSheetService
    {
        public void Clear(string sheetId, string range)
        {
        }

        public ValueRange GetColumns(string sheetId, string range)
        {
            return new ValueRange()
            {
                Values = new List<IList<object>>()
            };
        }

        public ValueRange GetRows(string sheetId, string range)
        {
            return new ValueRange()
            {
                Values = new List<IList<object>>()
            };
        }

        public void Update(ValueRange body, string sheetId, string range)
        {
        }
    }
}
