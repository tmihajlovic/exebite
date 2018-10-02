namespace ExeBite.Sheets.Common.Interfaces
{
    /// <summary>
    /// Interface for logging state and messages of the software.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the message with provided text
        /// </summary>
        /// <param name="text"></param>
        void LogInfo(string text);

        /// <summary>
        /// Logs error and text provided with it.
        /// </summary>
        /// <param name="text"></param>
        void LogError(string text);

        /// <summary>
        /// Logs warning with provided text.
        /// </summary>
        /// <param name="text"></param>
        void LogWarning(string text);
    }
}
