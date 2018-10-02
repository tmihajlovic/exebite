using System;
using System.Collections.Generic;
using System.Text;
using ExeBite.Sheets.Common.Interfaces;

namespace ConsoleDebug.Logger
{
    /// <summary>
    /// Console logging implementation of the logger that is to be used with Exebite Sheets
    /// </summary>
    class ConsoleLogger : ILogger
    {
        public void LogError(string text)
        {
            var backupTextColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("ERROR: {0}", text));
            Console.ForegroundColor = backupTextColor;
        }

        public void LogInfo(string text)
        {
            Console.WriteLine(string.Format("Info: {0}", text));
        }

        public void LogWarning(string text)
        {
            var backupTextColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("Warning: {0}", text));
            Console.ForegroundColor = backupTextColor;
        }
    }
}
