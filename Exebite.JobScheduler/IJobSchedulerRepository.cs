namespace Exebite.JobScheduler
{
    public interface IJobSchedulerRepository
    {
        void RegisterJobsToDB();

        void ScheduleJobCronExpression(string jobName, string jobGroup, string cronExpression, string name);

        void UnscheduleJob(string triggerName);

        void RemoveAllData();
    }
}
