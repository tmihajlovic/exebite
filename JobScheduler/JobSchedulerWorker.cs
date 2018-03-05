using Quartz;
using Quartz.Impl;

namespace Exebite.JobScheduler
{
    public class JobSchedulerWorker
    {
        private IScheduler scheduler;
        private ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

        public JobSchedulerWorker()
        {
            scheduler = schedulerFactory.GetScheduler().Result;
        }
        public void Start()
        {
            scheduler.Start();
            scheduler.Clear();
        }

        public void Stop()
        {
            scheduler.Shutdown();
        }
    }
}
