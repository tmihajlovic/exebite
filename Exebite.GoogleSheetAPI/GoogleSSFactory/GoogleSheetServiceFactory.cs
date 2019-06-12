using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace Exebite.GoogleSheetAPI.GoogleSSFactory
{
    public class GoogleSheetServiceFactory : IGoogleSheetServiceFactory
    {
        /// <summary>
        /// Annotate which scope(s) web application will use in regards to Google API.
        /// </summary>
        private readonly string[] scopes =
        {
            SheetsService.Scope.SpreadsheetsReadonly,
            SheetsService.Scope.DriveReadonly
        };

        /// <summary>
        /// Get a new Service Account credential-based SheetsService.
        /// </summary>
        /// <returns>Sheets service access.</returns>
        public SheetsService GetService()
        {
            using (var stream = new FileStream(Properties.Resources.SACredentialsLocation, FileMode.Open, FileAccess.Read))
            {
                return new SheetsService(
                      new BaseClientService.Initializer()
                      {
                          HttpClientInitializer = GoogleCredential
                              .FromStream(stream)
                              .CreateScoped(scopes)
                              .UnderlyingCredential as ServiceAccountCredential
                      }
                  );
            };
        }
    }
}