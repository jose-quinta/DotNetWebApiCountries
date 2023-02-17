using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Client.Controllers {
    public class HomeController : Controller {
        private readonly HttpClient _http = new HttpClient();
        private readonly HttpClientHandler _httpHandler = new HttpClientHandler();
        private readonly string _url = "https://localhost:7020/api/Country/";
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
            _httpHandler.ServerCertificateCustomValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public async Task<ActionResult<List<Country>>> Index() {
            var response = new List<Country>();
            using(var _api = await _http.GetAsync(_url)) {
                string _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<List<Country>>(_response);
            }
            return View(response);
        }
        public IActionResult Privacy() {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
