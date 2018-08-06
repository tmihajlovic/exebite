using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebClient.Models;

namespace WebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            var baseAddress = new Uri("http://localhost");
            var cookieContainer = new CookieContainer();

            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!HttpContext.Request.Cookies.TryGetValue("Identity.External", out string cookieValue))
                {
                    return RedirectToAction("Contact");
                }

                cookieContainer.Add(baseAddress, new Cookie("Identity.External", cookieValue));
                var result = await client.GetAsync("http://localhost:29796/api/location/Query?page=1&size=5");
                var pagingResult = await result.Content.ReadAsAsync<PagingResult<LocationDto>>();
                return View(pagingResult);
            }
        }

        // The server base address
        static string baseUrl = "https://www.googleapis.com/oauth2/v4/";

        // this will hold the Access Token returned from the server.
        static string accessToken = null;


        /// <summary>
        /// This method uses the OAuth Client Credentials Flow to get an Access Token to provide
        /// Authorization to the APIs.
        /// </summary>
        /// <returns></returns>
        private static async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                // We want the response to be JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Build up the data to POST.
                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                postData.Add(new KeyValuePair<string, string>("client_id", "287458637538-qa86tshchd7j6l6qfuv6tgedsvu1o9lg.apps.googleusercontent.com"));
                postData.Add(new KeyValuePair<string, string>("client_secret", "6lNLqakzb7yuep9yZzYXrXIB"));

                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

                // Post to the Server and parse the response.
                HttpResponseMessage response = await client.PostAsync("Token", content);
                string jsonString = await response.Content.ReadAsStringAsync();
                object responseData = JsonConvert.DeserializeObject(jsonString);

                // return the Access Token.
                return ((dynamic)responseData).access_token;
            }
        }

        /// <summary>
        /// Gets the page of Articles.
        /// </summary>
        /// <param name="page">The page to get.</param>
        /// <param name="tags">The tags to filter the articles with.</param>
        /// <returns>The page of articles.</returns>
        private static async Task<dynamic> GetArticles(int page, string tags)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                // create the URL string.
                string url = string.Format("v1/Articles?page={0}&tags={1}", page, HttpUtility.UrlEncode(tags));

                // make the request
                HttpResponseMessage response = await client.GetAsync(url);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                object responseData = JsonConvert.DeserializeObject(jsonString);
                return (dynamic)responseData;
            }
        }

        /// <summary>
        /// Gets the page of Questions.
        /// </summary>
        /// <param name="page">The page to get.</param>
        /// <param name="tags">The tags to filter the articles with.</param>
        /// <returns>The page of articles.</returns>
        private static async Task<dynamic> GetQuestions(int page, string tags)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                // create the URL string.
                string url = string.Format("v1/Questions/new?page={0}&include={1}", page, HttpUtility.UrlEncode(tags));

                // make the request
                HttpResponseMessage response = await client.GetAsync(url);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                object responseData = JsonConvert.DeserializeObject(jsonString);
                return (dynamic)responseData;
            }
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
