using System.Threading.Tasks;
using Exebite.Business.GoogleApiImportExport;
using Quartz;

namespace Exebite.JobScheduler.Jobs
{
    public class UpdateDailyMenus : IJob
    {
        private IGoogleDataImporter _googleDataImporter;

        public UpdateDailyMenus(IGoogleDataImporter googleDataImporter)
        {
            _googleDataImporter = googleDataImporter;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => _googleDataImporter.UpdateRestorauntsMenu());
        }
    }
}
