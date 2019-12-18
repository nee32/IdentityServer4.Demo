using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Secure()
        {
            ViewData["Message"] = "Secure page.";

            return View();
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> CallApiUsingClientCredentials()
        {
            var client = new HttpClient();
            var tokenClient = new TokenClient(client, new TokenClientOptions()
            {
                Address = "http://localhost:5000/connect/token",
                ClientId = "mvc",
                ClientSecret = "secret",
            });
            var tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync("HybridAndClientCredentials");

            client.SetBearerToken(tokenResponse.AccessToken);
            var content = await client.GetStringAsync("http://localhost:5001/weatherforecast");

            var json = JsonSerializer.Serialize(content);
            return Content(json);
        }

        public async Task<IActionResult> CallApiUsingUserAccessToken()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var content = await client.GetStringAsync("http://localhost:5001/weatherforecast");

            var json = JsonSerializer.Serialize(content);
            return Content(json);
        }
    }
}
