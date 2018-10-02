namespace ExeBite.Sheets.Common.Util
{
    /// <summary>
    /// Basic Result class - just denotes success rate with no attached objects.
    /// </summary>
    public class Result
    {
        #region Constructor
        /// <summary>
        /// Protected constructor so that we can't randomly create the class
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        protected Result(bool success, string message)
        {
            IsSuccess = success;
            ErrorMessage = message;
        }
        #endregion

        #region Static methods for creating new instances
        /// <summary>
        /// Used to create failed result.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result Fail(string errorMessage) => new Result(false, errorMessage);

        /// <summary>
        /// Used to create successful result.
        /// </summary>
        /// <returns></returns>
        public static Result Success() => new Result(true, string.Empty);
        #endregion


        #region Public properties
        /// <summary>
        /// Tells us if the result is Successful
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Tells us if the result has failed
        /// </summary>
        public bool IsFailure
        {
            get { return !IsSuccess; }
        }

        /// <summary>
        /// Provides error message in case of failed result.
        /// </summary>
        public string ErrorMessage { get; private set; } 
        #endregion

    }

    /// <summary>
    /// Result class that takes generic object as a value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result
    {

        #region Public property
        /// <summary>
        /// Value provided with result.
        /// </summary>
        public T Value { get; private set; }
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor provided with 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="success"></param>
        /// <param name="message"></param>
        protected Result(T value, bool success, string message)
            : base(success, message) => Value = value;
        #endregion

        #region Static creational methods
        /// <summary>
        /// provides successful result with accompanying object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<T> Success(T value) => new Result<T>(value, true, string.Empty);

        /// <summary>
        /// provides failed result with accompanying object and message
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result<T> Fail(T value, string message) => new Result<T>(value, false, message);
        #endregion
    }
}
