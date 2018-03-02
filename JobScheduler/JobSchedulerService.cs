using JobScheduler.Jobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler
{
    public class JobSchedulerService
    {
        IScheduler scheduler;
        ISchedulerFactory schedulerFactory = new StdSchedulerFactory();


        public JobSchedulerService()
        {
                scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.Start();
        }

        public void Start()
        {
            try
            {
                
                


                var job = JobBuilder.Create<UpdateDailyMenus>()
                    .WithIdentity("UpdateDailyMenus", "GoogleSheets")
                    .Build();
                //var cronExpresion = "0 45 7 ? * MON,TUE,WED,THU,FRI *";
                var trigger = TriggerBuilder.Create()
                    .WithIdentity("MenuUpdateTime")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(40)
                    .RepeatForever())
                    //.WithSchedule(CronScheduleBuilder.CronSchedule(cronExpresion))
                    .Build();

                var shj = scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
                throw;
            }
        }

        public void Stop()
        {
            scheduler.Shutdown();
            Console.WriteLine("stop");
        }
    }
}
