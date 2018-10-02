using ExeBite.Sheets.Common;
using ExeBite.Sheets.Common.Interfaces;
using ExeBite.Sheets.Common.Util;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using System;

namespace ExeBite.Sheets.Reader
{
    /// <summary>
    /// Provides instance of the Google Sheets service.
    /// throws exceptions when unable to initialize it.
    /// </summary>
    internal class ServiceProvider
    {
        #region private members
        private ILogger _logger;
        private UserCredential _credential;
        private SheetsService _service; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="credentials"></param>
        public ServiceProvider(ILogger logger, UserCredential credential)
        {
            _logger = logger;
            _credential = credential;
        }
        #endregion


        #region Public methods
        /// <summary>
        /// Provides instance of SheetsService
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public SheetsService GetInstance()
        {
            // If already initialized
            if (_service != null)
            {
                return _service;
            }

            ///Otherwise Initialize
            var instanceResult = InitializeSheet();
            if (instanceResult.IsFailure)
            {
                throw new Exception(instanceResult.ErrorMessage);
            }

            _service = instanceResult.Value;
            return _service;
        } 
        #endregion

        #region private methods
        /// <summary>
        /// Initializes sheet and gives back result that denotes if the sheet service has been properly initialized.
        /// Does not rethrow exceptions found inside.
        /// </summary>
        /// <returns></returns>
        private Result<SheetsService> InitializeSheet()
        {
            try
            {
                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _credential,
                    ApplicationName = Constants.APP_NAME
                });

                if (service == null)
                {
                    // Catch happens in the same method and will return Result.Fail
                    throw new Exception("Could not initialize Sheet");
                }

                return Result<SheetsService>.Success(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Failed Sheets Service initialization with exception message: {0}", ex.Message));
                return Result<SheetsService>.Fail(null, ex.Message);
            }
        } 
        #endregion
    }
}
