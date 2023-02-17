using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProvinceController : ControllerBase {
        private readonly ApplicationDbContext _context;
        public ProvinceController(ApplicationDbContext context) => _context = context;
        [HttpGet]
        public async Task<ActionResult<List<Province>>> Get() {
            var response = await _context.Provinces.ToListAsync();

            if (response == null)
                return BadRequest("There is no province yet");

            foreach (var item in response) {
                var country = await _context.Countries.FindAsync(item.CountryId);
                country!.Provices = new List<Province>();
                item.Country = country;
            }

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Province>> GetById(int id) {
            var response = await _context.Provinces.FindAsync(id);

            if (response == null)
                return BadRequest($"The province does not exist or is {response}");

            var country = await _context.Countries.FindAsync(response.CountryId);
            country!.Provices = new List<Province>();
            response.Country = country;

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<List<Province>>> Post(ProvinceDto request) {
            if (request == null)
                return BadRequest($"The request is {request}");

            Province province = new Province() {
                Name = request.Name,
                CountryId = request.CountryId
            };

            await _context.AddAsync(province);
            await _context.SaveChangesAsync();

            var response = await _context.Provinces.ToListAsync();
            foreach (var item in response) {
                var country = await _context.Countries.FindAsync(item.CountryId);
                country!.Provices = new List<Province>();
                item.Country = country;
            }

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Province>> Put(int id, ProvinceDto request) {
            if (request == null)
                return BadRequest($"The request is {request}");

            var response = await _context.Provinces.FindAsync(id);
            if (response == null)
                return BadRequest($"The province does not exist or is {response}");

            response.Name = request.Name;
            response.CountryId = request.CountryId;

            await _context.SaveChangesAsync();

            var country = await _context.Countries.FindAsync(response.CountryId);
            country!.Provices = new List<Province>();
            response.Country = country;

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Province>> Delete(int id) {
            var response = await _context.Provinces.FindAsync(id);
            if (response == null)
                return BadRequest($"The province does not exist or is {response}");

            _context.Remove(response);
            await _context.SaveChangesAsync();

            var country = await _context.Countries.FindAsync(response.CountryId);
            country!.Provices = new List<Province>();
            response.Country = country;

            return Ok(response);
        }
    }
}