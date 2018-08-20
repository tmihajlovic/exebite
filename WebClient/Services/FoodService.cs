using Exebite.DtoModels;
using WebClient.Services.Core;
using WebClient.Wrappers;

namespace WebClient.Services
{
    public class FoodService : RestService<CreateFoodDto, FoodQueryModelDto, UpdateFoodDto, FoodDto>, IFoodService
    {
        private const string _resourceUri = "food";

        public FoodService(IHttpClientWrapper client) : base(_resourceUri, client)
        {
        }
    }
}