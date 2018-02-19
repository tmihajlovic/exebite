using GoogleSpreadsheetApi.Common.Attributes;
using System;

namespace GoogleSpreadsheetApi.Models
{
    public class OrderSpreadsheet
    {
        [Spreadsheet(Name ="Id")]
        public long OrderId { get; set; }

        [Spreadsheet(Name = "When")]
        public DateTime Date { get; set; }

        public long EmployeeId { get; set; }
    }
}
