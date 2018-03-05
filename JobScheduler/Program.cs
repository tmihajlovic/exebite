using System;
using Topshelf;

namespace Exebite.JobScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<JobSchedulerWorker>(s =>
                {
                    s.ConstructUsing(name => new JobSchedulerWorker());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop()); 
                    
                });
                x.RunAsLocalService();
                x.SetDescription("Job Scheduler Service");
                x.SetDisplayName("Job Scheduler Service");
                x.SetServiceName("JobSchedulerService");
                
            });
            
            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
