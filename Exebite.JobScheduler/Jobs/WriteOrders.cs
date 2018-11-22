using System.Threading.Tasks;
using Exebite.Business.GoogleApiImportExport;
using Quartz;

namespace Exebite.JobScheduler.Jobs
{
    public class WriteOrders : IJob
    {
        private readonly IGoogleDataExporter _googleDataExporter;

        public WriteOrders(IGoogleDataExporter googleDataExporter)
        {
            _googleDataExporter = googleDataExporter;
        }

#pragma warning disable ASYNC0001 // Asynchronous method names should end with Async This is from library implementation

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => _googleDataExporter.PlaceOrdersForRestaurant("Restoran pod Lipom"));
        }
#pragma warning restore ASYNC0001 // Asynchronous method names should end with Async
    }
}
