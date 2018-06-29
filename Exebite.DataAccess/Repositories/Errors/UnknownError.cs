namespace Exebite.DataAccess.Repositories
{
    public class UnknownError : Error
    {
        public UnknownError(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
