using System.Threading.Tasks;
using Exebite.Business.GoogleApiImportExport;
using Quartz;

namespace Exebite.JobScheduler.Jobs
{
    public class UpdateDailyMenus : IJob
    {
        private readonly IGoogleDataImporter _googleDataImporter;

        public UpdateDailyMenus(IGoogleDataImporter googleDataImporter)
        {
            _googleDataImporter = googleDataImporter;
        }

#pragma warning disable ASYNC0001 // Asynchronous method names should end with Async This is from library implementation
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => _googleDataImporter.UpdateRestaurantMenu());
        }
#pragma warning restore ASYNC0001 // Asynchronous method names should end with Async
    }
}
