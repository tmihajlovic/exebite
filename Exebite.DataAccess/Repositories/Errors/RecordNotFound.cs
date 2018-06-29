namespace Exebite.DataAccess.Repositories
{
    public class RecordNotFound : Error
    {
        private readonly string message;

        public RecordNotFound(string message)
        {
            this.message = message;
        }
    }
}
