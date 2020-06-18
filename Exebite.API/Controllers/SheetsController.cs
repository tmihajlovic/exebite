using System;
using Exebite.GoogleSheetAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("/api/sheets")]
    public class SheetsController : ControllerBase
    {
        private readonly ILogger<SheetsController> _logger;
        private readonly IGoogleSheetAPIService _apiService;

        public SheetsController(
            ILogger<SheetsController> logger,
            IGoogleSheetAPIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        [HttpGet("fetch")]
        public JsonResult FetchData()
        {
            try
            {
                _apiService.UpdateCustomers();
                _apiService.UpdateDailyMenuLipa();
                _apiService.UpdateDailyMenuTopliObrok();
                _apiService.UpdateDailyMenuParrilla();
                _apiService.UpdateMainMenuIndex();
                _apiService.UpdateDailyMenuSerpica();
                _apiService.UpdateMainMenuHeyDay();
                _apiService.UpdateMainMenuParrilla();
                _apiService.UpdateDailyMenuMimas();

                _logger.LogInformation("Successfully fetched and updated DB information from Google Sheets.");
                return new JsonResult(new
                {
                    success = true,
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new JsonResult(new
                {
                    success = false,
                    message = "Error occurred trying to fetch the data"
                });
            }
        }
    }
}