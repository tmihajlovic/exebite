using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace Exebite.JobScheduler
{
    public class JobSchedulerService
    {
        private IScheduler _scheduler;
        private ISchedulerFactory _schedulerFactory = new StdSchedulerFactory();

        public JobSchedulerService()
        {
            _scheduler = _schedulerFactory.GetScheduler().Result;

        }
        public void Start()
        {
            Task.Run(() => _scheduler.Start());
            //_scheduler.TriggerJob(new JobKey("WriteOrders", "GoogleSheets"));
            //JobSchedulerRepository jsb = new JobSchedulerRepository();
            //_scheduler.Clear();
            //jsb.RegisterJobsToDB();
            //var test = TriggerBuilder.Create()
            //     .WithIdentity("Test")
            //     .ForJob(new JobKey("WriteOrders", "GoogleSheets"))
            //     .StartNow()
            //     .WithCronSchedule("0 3 14 ? * MON,TUE,WED,THU,FRI *")
            //     .Build();
            //_scheduler.ScheduleJob(test);
            //var test1 = TriggerBuilder.Create()
            //     .WithIdentity("Test")
            //     .ForJob(new JobKey("WriteOrders", "GoogleSheets"))
            //     .StartNow()
            //     .WithCronSchedule("0 10 14 ? * MON,TUE,WED,THU,FRI *")
            //     .Build();
            //_scheduler.ScheduleJob(test1);
        }

        public void Stop()
        {
            _scheduler.Shutdown();
        }
    }
}
