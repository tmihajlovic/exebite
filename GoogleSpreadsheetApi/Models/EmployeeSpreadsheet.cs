using GoogleSpreadsheetApi.Common.Attributes;
namespace GoogleSpreadsheetApi.Models
{
    //[Spreadsheet(Name = "Employee")]
    public class EmployeeSpreadsheet
    {
        [Spreadsheet(Name="Id")]
        public long EmployeeId { get; set; }

        [Spreadsheet(Name="Full Name")]
        public string Name { get; set; }
    }
}
