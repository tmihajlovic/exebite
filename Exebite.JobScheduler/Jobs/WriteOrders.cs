using System.Threading.Tasks;
using Exebite.Business.GoogleApiImportExport;
using Quartz;

namespace Exebite.JobScheduler.Jobs
{
    public class WriteOrders : IJob
    {
        private IGoogleDataExporter _googleDataExporter;

        public WriteOrders(IGoogleDataExporter googleDataExporter)
        {
            _googleDataExporter = googleDataExporter;
        }

        public Task Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
