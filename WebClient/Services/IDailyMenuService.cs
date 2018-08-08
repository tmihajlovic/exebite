using Exebite.DtoModels;
using WebClient.Services.Core;

namespace WebClient.Services
{
    public interface IDailyMenuService : IRestService<CreateDailyMenuDto, DailyMenuQueryDto, UpdateDailyMenuDto, DailyMenuDto>
    {
    }
}
