using Quartz;
using Quartz.Impl;

namespace Exebite.JobScheduler
{
    public class JobSchedulerService
    {
        private IScheduler scheduler;
        private ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

        public JobSchedulerService()
        {
            scheduler = schedulerFactory.GetScheduler().Result;
        }
        public void Start()
        {
            scheduler.Start();
        }

        public void Stop()
        {
            scheduler.Shutdown();
        }
    }
}
