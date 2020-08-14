using System.Threading.Tasks;
using Exebite.Business;
using Quartz;

namespace Exebite.JobScheduler.Jobs
{
    public class UpdateDailyMenus : IJob
    {
        private readonly IRestaurantService _restaurantService;

        public UpdateDailyMenus(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

#pragma warning disable ASYNC0001 // Asynchronous method names should end with Async This is from library implementation
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => _restaurantService.UpdateRestaurantMenu());
        }
#pragma warning restore ASYNC0001 // Asynchronous method names should end with Async
    }
}
