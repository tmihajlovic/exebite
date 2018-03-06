using Exebite.JobScheduler.Jobs;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace Exebite.JobScheduler
{
    public class JobSchedulerRepository : IJobSchedulerRepository
    {
        private IScheduler scheduler;
        private ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

        public JobSchedulerRepository()
        {
            scheduler = schedulerFactory.GetScheduler().Result;
        }
        
        /// <summary>
        /// Adds jobs to database
        /// </summary>
        public void RegisterJobsToDB()
        {
            var UpdateDailyMenusJob = JobBuilder.Create<UpdateDailyMenus>()
                .WithIdentity("UpdateDailyMenus", "GoogleSheets")
                .StoreDurably()
                .Build();
            scheduler.AddJob(UpdateDailyMenusJob, true);
            
            var WriteOrders = JobBuilder.Create<WriteOrders>()
                .WithIdentity("WriteOrders", "GoogleSheets")
                .StoreDurably()
                .Build();
            scheduler.AddJob(WriteOrders, true);
        }

        /// <summary>
        /// Schedule job base on cron expresion
        /// </summary>
        /// <param name="job">Job to be schedule</param>
        /// <param name="cronExpresion">Cron expresion</param>
        /// <param name="name">Name of trigger</param>
        public void ScheduleJobCronExpresion(string jobName, string jobGroup, string cronExpresion, string name)
        {
            var jobKey = new JobKey(jobName,jobGroup);
            var _cronExpresion = cronExpresion; // "0 45 7 ? * MON,TUE,WED,THU,FRI *";
            var trigger = TriggerBuilder.Create()
                .WithIdentity(name)
                .ForJob(jobKey)
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule(_cronExpresion))
                .Build();
            if (!scheduler.CheckExists(trigger.Key).Result)
            {
                scheduler.ScheduleJob(trigger);
            }
        }

        /// <summary>
        /// Remuves all data from the database
        /// </summary>
        public void RemoveAllData()
        {

            scheduler.Clear();
        }

        /// <summary>
        /// Unschedule job by removing trigger
        /// </summary>
        /// <param name="triggerName">Trigger name</param>
        public void UnscheduleJob(string triggerName)
        {
            TriggerKey triggerKey = new TriggerKey(triggerName);
            scheduler.UnscheduleJob(triggerKey);
        }
    }
}
