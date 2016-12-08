using Microsoft.AspNetCore.Mvc;
using WhereWereWe.Domain.Models;
using WhereWereWe.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WhereWereWe.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SeriesController : Controller
    {
        private ISeriesRepository _seriesRepository;

        public SeriesController(ISeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Series>> Get()
        {
            return await _seriesRepository.GetSeries();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Series series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _seriesRepository.AddSeries(series);

            return Ok();
        }
    }
}
