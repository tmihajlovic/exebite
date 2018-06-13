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

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => _googleDataExporter.PlaceOrdersForRestaurant("Restoran pod Lipom"));
        }
    }
}
