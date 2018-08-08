using Exebite.DtoModels;
using WebClient.Services.Core;
using WebClient.Wrappers;

namespace WebClient.Services
{
    public class DailyMenuService : RestService<CreateDailyMenuDto, DailyMenuQueryDto, UpdateDailyMenuDto, DailyMenuDto>, IDailyMenuService
    {
        private const string _resourceUri = "dailymenu";

        public DailyMenuService(IHttpClientWrapper client) : base(_resourceUri, client)
        {
        }

    }
}
