using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleSpreadsheetApi.GoogleSSFactory
{
    public interface IGoogleSheetServiceFactory
    {
        SheetsService GetService();
    }
}
