using System;
using Exebite.Business.GoogleApiImportExport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("/api/sheets")]
    public class SheetsController : ControllerBase
    {
        private readonly ILogger<SheetsController> _logger;
        private readonly IGoogleDataImporter _googleDataImporter;

        public SheetsController(
            IGoogleDataImporter googleDataImporter,
            ILogger<SheetsController> logger)
        {
            _logger = logger;
            _googleDataImporter = googleDataImporter;
        }

        [HttpGet("fetch")]
        public JsonResult FetchData()
        {
            try
            {
                _googleDataImporter.UpdateRestorauntsMenu();

                _logger.LogInformation("Successfully fetched and updated DB information from Google Sheets.");
                return new JsonResult(new
                {
                    success = true
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