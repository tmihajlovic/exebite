using GoogleSpreadsheetApi.Common.Attributes;
using System;

namespace GoogleSpreadsheetApi.Models
{
    [Spreadsheet(Name = "Food")]
    public class FoodSpreadsheet
    {
        [Spreadsheet(Name="Id")]
        public long FoodId { get; set; }

        public string Name { get; set; }

        [Spreadsheet(Name="Value")]
        public double Price { get; set; }

        public string Date { get; set; }

        public bool IsOrderd { get; set; }
    }
}
