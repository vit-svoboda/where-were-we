using Microsoft.AspNetCore.Mvc;
using WhereWereWe.Domain.Models;
using System.Threading.Tasks;
using WhereWereWe.Domain.Interfaces;
using System;
using WhereWereWe.Api.Models;
using AutoMapper;

namespace WhereWereWe.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProgressController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProgressTrackingService progressService;
        private readonly IUserService userService;

        public ProgressController(IProgressTrackingService progressService, IUserService userService, IMapper mapper)
        {
            this.progressService = progressService;
            this.userService = userService;

            this.mapper = mapper;
        }

        public async Task<IActionResult> Get()
        {
            var user = await userService.GetUser(HttpContext.User);
            var progress = await progressService.GetAllProgress(user);

            // TODO: View model!
            return Ok(progress);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Guid seriesId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userService.GetUser(HttpContext.User);

            var progress = await progressService.StartProgress(user, seriesId);

            return Created($"api/{progress.Id}", progress);
        }

        [HttpPut, Route("{id:guid}")]
        public async Task<IActionResult> Put(ProgressUpdateViewModel progressViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userService.GetUser(HttpContext.User);

            await progressService.UpdateProgress(user, mapper.Map<SeriesProgress>(progressViewModel));

            return Ok();
        }
    }
}
