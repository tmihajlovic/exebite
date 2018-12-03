using Exebite.Sheets.Common;
using Exebite.Sheets.Common.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;

namespace Exebite.Sheets.Reader
{
    /// <summary>
    /// Provides User credentials for google authentication services
    /// </summary>
    internal class Credentials
    {
        #region Public Properties
        /// <summary>
        /// Used to provide user credentials for logging into Google authentication service.
        /// </summary>
        public UserCredential UserCredential { get; private set; } 
        #endregion

        #region Constructor
        /// <summary>
        /// Consturctor that takes Logger and Scope for the credentials.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="scopes"></param>
        public Credentials(ILogger logger, string[] scopes)
        {
            try
            {
                using (var stream =
                        new FileStream(Configuration.CredentialsLocation, FileMode.Open, FileAccess.Read))
                {
                    string credPath = Configuration.TokenLocation;
                    UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                   logger.LogInfo("Log in successful!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format("Exception details: {0}", ex.Message));
                throw;
            }
        } 
        #endregion

    }
}
