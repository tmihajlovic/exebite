using Exebite.DtoModels;
using WebClient.Services.Core;

namespace WebClient.Services
{
    public interface IFoodService : IRestService<CreateFoodDto, FoodQueryModelDto, UpdateFoodDto, FoodDto>
    {
    }
}