namespace Exebite.DataAccess.Repositories
{
    public class ValidationError : Error
    {
        public ValidationError(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
