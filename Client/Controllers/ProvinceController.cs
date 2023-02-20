using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Client.Controllers {
    public class ProvinceController : Controller {
        private readonly HttpClient _http = new HttpClient();
        private readonly HttpClientHandler _httpHandler = new HttpClientHandler();
        private readonly string _url = "https://localhost:7020/api/Province/";
        public ProvinceController() => _httpHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        public async Task<ActionResult<List<ProvinceController>>> Index(string? search) {
            var response = new List<Province>();
            using(var _api = await _http.GetAsync(_url)) {
                string _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<List<Province>>(_response)!;
            }

            if (!String.IsNullOrEmpty(search)) {
                var _response = from item in response where item.Name.Contains(search) select item;
                return View(_response);
            }

            return View(response);
        }
        public async Task<ActionResult<Province>> Details(int id) {
            var response = new Province();
            using(var _api = await _http.GetAsync(_url + id)) {
                string _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Province>(_response)!;
            }

            if (response == null)
                return View();

            return View(response);
        }
        public ActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<List<Province>>> Create(Province request) {
            ProvinceDto _request = new ProvinceDto() {
                Name = request.Name,
                CountryId = request.CountryId
            };

            var response = new List<Province>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(_request), Encoding.UTF8, "application/json");
            using(var _api = await _http.PostAsync(_url, content)) {
                string _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<List<Province>>(_response)!;
            }

            if (response == null)
                return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult<Province>> Edit(int id) {
            var response = new Province();
            using (var _api = await _http.GetAsync(_url + id)) {
                string _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Province>(_response)!;
            }

            if (response == null)
                return View();

            return View(response);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Province>> Edit(Province request) {
            ProvinceDto _request = new ProvinceDto() {
                Name = request.Name,
                CountryId = request.CountryId
            };

            var response = new Province();

            StringContent content = new StringContent(JsonConvert.SerializeObject(_request), Encoding.UTF8, "application/json");
            using (var _api = await _http.PutAsync(_url + request.Id, content)) {
                if (!_api.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult<Province>> Delete(int id) {
            var response = new Province();
            using (var _api = await _http.GetAsync(_url + id)) {
                string _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Province>(_response)!;
            }

            if (response == null)
                return View();

            return View(response);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Province>> Delete(int id, Province request) {
            var response = new Province();
            using (var _api = await _http.DeleteAsync(_url + id)) {
                string _response = await _api.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Province>(_response)!;
            }

            if (response == null)
                return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Index));
        }
    }
}