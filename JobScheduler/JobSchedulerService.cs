using Exebite.JobScheduler.Jobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

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
            try
            {
                Task.Run(() => scheduler.Start());
                AddJobs();
                TestAddCronDailyMenu();
                TestAddCronDailyWrite();
                //TestExecuteWriteNow();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
                throw;
            }
        }

        public void AddJobs()
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

        public void Stop()
        {
            scheduler.Shutdown();
        }

        /// <summary>
        /// Schedule job base on cron expresion
        /// </summary>
        /// <param name="job">Job to be schedule</param>
        /// <param name="cronExpresion">Cron expresion</param>
        /// <param name="name">Name of trigger</param>
        public void ScheduleJobCronExpresion(IJobDetail job, string cronExpresion, string name)
        {
            var _cronExpresion = cronExpresion; // "0 45 7 ? * MON,TUE,WED,THU,FRI *";
            var trigger = TriggerBuilder.Create()
                .WithIdentity(name)
                .ForJob(job)
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
        /// Executes job
        /// </summary>
        /// <param name="jobName">Name of job</param>
        /// <param name="jobGroup">Name of job group</param>
        public void ExecuteJobNow(string jobName, string jobGroup)
        {
            var key = new JobKey(jobName, jobGroup);
            if (scheduler.CheckExists(key).Result)
            {
                scheduler.TriggerJob(key);
            }
        }

        //TEST 

        public void TestAddCronDailyMenu()
        {

            //FOR TEST, will go dynamicly in prod
            JobKey key = new JobKey("UpdateDailyMenus", "GoogleSheets");
            var job = scheduler.GetJobDetail(key).Result;
            var cron = "0 45 7 ? * MON,TUE,WED,THU,FRI *";
            var triggerName = "UpdateDailyMenus-0745-workdays";
            ScheduleJobCronExpresion(job, cron, triggerName);
        }

        public void TestAddCronDailyWrite()
        {
            JobKey key = new JobKey("WriteOrders", "GoogleSheets");
            var job = scheduler.GetJobDetail(key).Result;
            var cron = "0 53 15 ? * MON,TUE,WED,THU,FRI *";
            var triggerName = "WriteOrdersDaily-1005-workdays";
            ScheduleJobCronExpresion(job, cron, triggerName);
        }

        public void TestExecuteWriteNow()
        {
            var name = "WriteOrders";
            var group = "GoogleSheets";
            ExecuteJobNow(name, group);
        }

        public void TestExternalSetup()
        {
            Task.Run(() => scheduler.Start());
            AddJobs();
            TestAddCronDailyMenu();
            TestAddCronDailyWrite();
            scheduler.Shutdown();
        }
    }
}
