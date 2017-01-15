using Microsoft.AspNetCore.Mvc;
using WhereWereWe.Domain.Models;
using WhereWereWe.Domain.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WhereWereWe.Api.Models;
using AutoMapper;

namespace WhereWereWe.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SeriesController : Controller
    {
        private readonly IMapper mapper;
        private ISeriesRepository seriesRepository;

        public SeriesController(ISeriesRepository seriesRepository, IMapper mapper)
        {
            this.seriesRepository = seriesRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var series = await seriesRepository.GetSeries();
            return Ok(series);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewSeriesViewModel series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdSeries = await seriesRepository.AddSeries(mapper.Map<Series>(series));

            return Created($"/api/series/{createdSeries.Id}", createdSeries);
        }
    }
}
