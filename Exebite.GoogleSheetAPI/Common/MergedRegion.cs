using Google.Apis.Sheets.v4.Data;
using System.Text;

namespace Exebite.GoogleSheetAPI.Common
{
    /// <summary>
    /// This class represents a merged region in the Google Sheet
    /// Since we need more information that regular GridRange provides
    /// This is used as a wrapper around it.
    /// </summary>
    public class MergedRegion
    {
        #region privatge members
        private readonly Sheet _sheet;
        #endregion

        #region Public properties
        /// <summary>
        /// GridRange of the Merged region.
        /// </summary>
        public GridRange Range { get; private set; }

        /// <summary>
        /// A1 Notation of the first cell location.
        /// </summary>
        public string A1FirstCell { get; private set; }

        /// <summary>
        /// Name of the sheet. Used to construct queries.
        /// We don't need to expose entire sheet.
        /// </summary>
        public string SheetName
        {
            get
            {
                return _sheet.Properties.Title;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor for the class.
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="range"></param>
        public MergedRegion(Sheet sheet, GridRange range)
        {
            _sheet = sheet;
            Range = range;

            A1FirstCell = CalculateFirstCellA1();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Calculates A1 Notation for the first cell.
        /// </summary>
        /// <returns></returns>
        private string CalculateFirstCellA1()
        {
            var sb = new StringBuilder();
            sb.Append("'");
            sb.Append(_sheet.Properties.Title);
            sb.Append("'");
            sb.Append("!");
            sb.Append(A1Notation.ToCellFormat(
                    Range.StartColumnIndex.Value,
                    Range.StartRowIndex.Value));

            return sb.ToString();
        }
        #endregion
    }
}
