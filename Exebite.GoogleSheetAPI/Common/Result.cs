namespace Exebite.GoogleSheetAPI.Common
{
    /// <summary>
    /// Basic Result class - just denotes success rate with no attached objects.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// Protected constructor so that we can't randomly create the class
        /// </summary>
        /// <param name="success">True if successfully</param>
        /// <param name="message">Error message.</param>
        protected Result(bool success, string message)
        {
            IsSuccess = success;
            ErrorMessage = message;
        }

        /// <summary>
        /// Used to create failed result.
        /// </summary>
        /// <param name="errorMessage">Error message.</param>
        /// <returns>Result with not success and error message.</returns>
        public static Result Fail(string errorMessage) => new Result(false, errorMessage);

        /// <summary>
        /// Used to create successful result.
        /// </summary>
        /// <returns>Result with success and no error message.</returns>
        public static Result Success() => new Result(true, string.Empty);

        /// <summary>
        /// Gets a value indicating whether the result is Successful
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the result has failed
        /// </summary>
        public bool IsFailure
        {
            get { return !IsSuccess; }
        }

        /// <summary>
        /// Gets provides error message in case of failed result.
        /// </summary>
        public string ErrorMessage { get; private set; }
    }

    /// <summary>
    /// Result class that takes generic object as a value.
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// Value provided with result.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Constructor provided with
        /// </summary>
        /// <param name="value"></param>
        /// <param name="success"></param>
        /// <param name="message"></param>
        protected Result(T value, bool success, string message)
            : base(success, message) => Value = value;

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
    }
}
