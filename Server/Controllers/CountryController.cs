using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase {
        private readonly ApplicationDbContext _context;
        public CountryController(ApplicationDbContext context) => _context = context;
        [HttpGet]
        public async Task<ActionResult<List<Country>>> Get() {
            var response = await _context.Countries.ToListAsync();

            if (response == null)
                return BadRequest("There is no country yet");

            foreach (var item in response) {
                item.Provices = new List<Province>();
            }

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetById(int id) {
            var response = await _context.Countries.FindAsync(id);

            if (response == null)
                return BadRequest($"The country does not exist or is {response}");

            var provinces = await _context.Provinces.Where(x => x.CountryId == id).ToListAsync();
            foreach (var item in provinces) {
                item.Country = new Country();
            }

            response.Provices = provinces;
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<List<Country>>> Post(CountryDto request) {
            if (request == null)
                return BadRequest($"The request is {request}");

            Country country = new Country() {
                Name = request.Name
            };

            await _context.AddAsync(country);
            await _context.SaveChangesAsync();

            var response = await _context.Countries.ToListAsync();
            foreach (var item in response) {
                item.Provices = new List<Province>();
            }

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Country>> Put(int id, CountryDto request) {
            if (request == null)
                return BadRequest($"The request is {request}");

            var response = await _context.Countries.FindAsync(id);
            if (response == null)
                return BadRequest($"The country does not exist or is {response}");

            response.Name = request.Name;

            await _context.SaveChangesAsync();

            var provinces = await _context.Provinces.Where(x => x.CountryId == id).ToListAsync();
            response.Provices = provinces;

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> Delete(int id) {
            var response = await _context.Countries.FindAsync(id);
            if (response == null)
                return BadRequest($"The country does not exist or is {response}");

            _context.Remove(response);
            await _context.SaveChangesAsync();

            var provinces = await _context.Provinces.Where(x => x.CountryId == id).ToListAsync();
            response.Provices = provinces;

            return Ok(response);
        }
    }
}