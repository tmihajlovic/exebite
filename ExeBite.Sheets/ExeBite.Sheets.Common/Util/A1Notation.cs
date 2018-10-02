using System.Text;

namespace ExeBite.Sheets.Common.Util
{
    /// <summary>
    /// A1 Notation is the system that Google Sheets use for managing Cell.
    /// </summary>
    public class A1Notation
    {
        /// <summary>
        /// There is 26 letters used in sheet: ABCDEFGHIJKLMNOPQRSTUVWXYZ
        /// </summary>
        private const int lettersScope = 26;

        /// <summary>
        /// Since "A" is on position 65 when converting to char, we need to offset everything by 64
        /// that way, position 1 will be A, position 2 will be B etc.
        /// </summary>
        private const int digitsOffset = 64;

        /// <summary>
        /// Since encoding starts from 0, on the last digit we need to offset all 65 posisitons
        /// so 0 will be A, 1 will be B etc.
        /// </summary>
        private const int lastDigitOffset = 65;

        /// <summary>
        /// Takes column and row and gives back A1 notation format
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static string ToCellFormat(int column, int row)
        {
            StringBuilder sb = new StringBuilder();
            var firstDigit = column / lettersScope;
            var secondDigit = column % lettersScope;

            if (firstDigit > 0)
            {
                // We need 1 to be A so we increase by 64
                char firstChar = (char)(firstDigit + digitsOffset);
                sb.Append(firstChar);
            }

            // We need 0 to be A so we increase by 65
            char secondChar = (char)(secondDigit + lastDigitOffset);
            sb.Append(secondChar);

            // we need to shift row by 1 since it starts indexing at 0
            // and in A1 notation it starts at 1
            sb.Append(row+1);

            return sb.ToString();
        }

        /// <summary>
        /// Gives range format in example of A1:B2 for the ranges.
        /// </summary>
        /// <param name="startColumn"></param>
        /// <param name="startRow"></param>
        /// <param name="endColumn"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static string ToRangeFormat(int startColumn, int startRow, int endColumn, int endRow)
        {
            var sb = new StringBuilder();
            sb.Append(ToCellFormat(startColumn, startRow));
            sb.Append(":");
            sb.Append(ToCellFormat(endColumn - 1, endRow - 1));
            return sb.ToString();
        }
    }
}
