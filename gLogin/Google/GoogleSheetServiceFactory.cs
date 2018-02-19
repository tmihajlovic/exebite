using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace gLogin.Google
{
    public static class GoogleSheetServiceFactory
    {
        public static SheetsService GetService(string json)
        {
            UserCredential credential;
            using (var stream = new FileStream(json, FileMode.Open, FileAccess.Read))
            {

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { "https://www.googleapis.com/auth/spreadsheets" },
                    "user",
                    CancellationToken.None
                ).Result;

            }

            SheetsService SSservice = new SheetsService(new BaseClientService.Initializer()
            {

                HttpClientInitializer = credential,
                ApplicationName = "LunchOrder"

            });

            return SSservice;
        }
    }
}