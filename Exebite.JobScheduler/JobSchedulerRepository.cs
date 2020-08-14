﻿using Exebite.JobScheduler.Jobs;
using Quartz;
using Quartz.Impl;

namespace Exebite.JobScheduler
{
    public class JobSchedulerRepository : IJobSchedulerRepository
    {
        private readonly IScheduler scheduler;
        private readonly ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

        public JobSchedulerRepository()
        {
            scheduler = schedulerFactory.GetScheduler().Result;
        }

        /// <summary>
        /// Adds jobs to database
        /// </summary>
        public void RegisterJobsToDB()
        {
            var updateDailyMenusJob = JobBuilder.Create<UpdateDailyMenus>()
                .WithIdentity("UpdateDailyMenus", "GoogleSheets")
                .StoreDurably()
                .RequestRecovery(true)
                .Build();
            scheduler.AddJob(updateDailyMenusJob, true);
        }

        /// <summary>
        /// Schedule job base on cron expression
        /// </summary>
        /// <param name="jobName">Job to be schedule</param>
        /// <param name="jobGroup">Job group</param>
        /// <param name="cronExpression">Cron expression</param>
        /// <param name="name">Name of trigger</param>
        public void ScheduleJobCronExpression(string jobName, string jobGroup, string cronExpression, string name)
        {
            var jobKey = new JobKey(jobName, jobGroup);
            var trigger = TriggerBuilder.Create()
                .WithIdentity(name)
                .ForJob(jobKey)
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule(cronExpression))
                .Build();
            if (!scheduler.CheckExists(trigger.Key).Result)
            {
                scheduler.ScheduleJob(trigger);
            }
        }

        /// <summary>
        /// Removes all data from the database
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
