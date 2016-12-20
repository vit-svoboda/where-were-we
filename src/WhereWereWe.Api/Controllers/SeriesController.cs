using Microsoft.AspNetCore.Mvc;
using WhereWereWe.Domain.Models;
using WhereWereWe.Domain.Interfaces;
using System.Collections.Generic;
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
        public async Task<IEnumerable<Series>> Get()
        {
            return await seriesRepository.GetSeries();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewSeriesViewModel series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await seriesRepository.AddSeries(mapper.Map<Series>(series));

            return Ok();
        }
    }
}
