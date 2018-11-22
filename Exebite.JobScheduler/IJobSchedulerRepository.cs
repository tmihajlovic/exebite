namespace Exebite.JobScheduler
{
    public interface IJobSchedulerRepository
    {
        /// <summary>
        /// Register jobs to DB with no triggers. New jobs need to be added to method body
        /// </summary>
        void RegisterJobsToDB();

        /// <summary>
        /// Make new trigger for job and save it to DB
        /// </summary>
        /// <param name="jobName">Name of job to be executed</param>
        /// <param name="jobGroup">Group of job to be executed</param>
        /// <param name="cronExpression">Cron expresion</param>
        /// <param name="name">Name of trigger</param>
        /// <remarks>Cron expresion repersent formula for calulating execution times <seealso cref="http://www.cronmaker.com/"/></remarks>
        void ScheduleJobCronExpression(string jobName, string jobGroup, string cronExpression, string name);

        /// <summary>
        /// Unschedule time by given trigger name
        /// </summary>
        /// <param name="triggerName">Name of trigger</param>
        /// <remarks>Job is not deleted from DB, trigger is</remarks>
        void UnscheduleJob(string triggerName);

        /// <summary>
        /// Remove all data from scheduler database
        /// </summary>
        void RemoveAllData();
    }
}
