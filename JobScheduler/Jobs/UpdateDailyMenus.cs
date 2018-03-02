using Exebite.Business.GoogleApiImportExport;
using JobScheduler.Unity;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace JobScheduler.Jobs
{
    public class UpdateDailyMenus : IJob
    {
        IGoogleApiOldSheets _oldSheets;

        public UpdateDailyMenus()
        {
            _oldSheets = UnityConfig.Container.Resolve<IGoogleApiOldSheets>();
        }
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() => _oldSheets.WriteOrdersToSheets());
            return task;
        }
    }
}

