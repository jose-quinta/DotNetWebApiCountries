using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Client.Controllers {
    public class CountryController : Controller {
        private readonly HttpClient _http = new HttpClient();
        private readonly HttpClientHandler _httpHandler = new HttpClientHandler();
        private readonly string _url = "https://localhost:7020/api/Country/";
        public CountryController() => _httpHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
       /*  public string Welcome(string type, int id = 1) {
            return HtmlEncoder.Default.Encode($"~/Country/_Country{type}, id={id}");
        } */
        public async Task<ActionResult<List<Country>>> Index(string? search) {
            var response = new List<Country>();
            using(var _api = await _http.GetAsync(_url)) {
                String _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<List<Country>>(_response)!;
            }

            if (!String.IsNullOrEmpty(search)) {
                var _response = from item in response where item.Name.Contains(search) select item;
                return View(_response);
            }

            return View(response);
        }
        public async Task<ActionResult> CountryList() {
            var response = new List<Country>();
            using (var _api = await _http.GetAsync(_url)) {
                String _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<List<Country>>(_response)!;
            }

            return Json(new SelectList(response, "Id", "Name"));
        }
        public async Task<ActionResult<Country>> Details(int id) {
            var response = new Country();
            using(var _api = await _http.GetAsync(_url + id)) {
                String _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Country>(_response)!;
            }

            if (response == null)
                return View();

            return View(response);
        }
        public ActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<List<Country>>> Create(Country request) {
            CountryDto _request = new CountryDto() {
                Name = request.Name
            };

            var response = new List<Country>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(_request), Encoding.UTF8, "application/json");
            using (var _api = await _http.PostAsync(_url, content)) {
                String _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<List<Country>>(_response)!;
            }

            if (response == null)
                return View();

            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult<Country>> Edit(int id) {
            var response = new Country();
            using (var _api = await _http.GetAsync(_url + id)) {
                String _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Country>(_response)!;
            }

            if (response == null)
                return View();

            return View(response);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Country>> Edit(Country request) {
            CountryDto _request = new CountryDto() {
                Name = request.Name
            };

            var response = new Country();

            StringContent content = new StringContent(JsonConvert.SerializeObject(_request), Encoding.UTF8, "application/json");
            using (var _api = await _http.PutAsync(_url + request.Id , content)) {
                if (!_api.IsSuccessStatusCode)
                    return View();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult<Country>> Delete(int id) {
            var response = new Country();
            using (var _api = await _http.GetAsync(_url + id)) {
                String _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Country>(_response)!;
            }

            if (response == null)
                return View();

            return View(response);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Country>> Delete(int id, Country request) {
            var response = new Country();
            using(var _api = await _http.DeleteAsync(_url + id)) {
                String _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Country>(_response)!;
            }

            if (response == null)
                return View();

            return RedirectToAction(nameof(Index));
        }
    }
}