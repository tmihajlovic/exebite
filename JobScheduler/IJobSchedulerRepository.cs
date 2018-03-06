using Quartz;

namespace Exebite.JobScheduler
{
    public interface IJobSchedulerRepository
    {
        void RegisterJobsToDB();
        void ScheduleJobCronExpresion(string jobName, string jobGroup, string cronExpresion, string name);
        void UnscheduleJob(string triggerName);
        void RemoveAllData();
    }
}
