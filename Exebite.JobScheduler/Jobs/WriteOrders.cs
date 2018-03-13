using System.Threading.Tasks;
using Quartz;

namespace Exebite.JobScheduler.Jobs
{
    public class WriteOrders : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
