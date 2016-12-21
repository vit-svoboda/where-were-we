using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhereWereWe.Domain.Interfaces;

namespace WhereWereWe.Api.Controllers
{
    [Route("/api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly IUserRepository userRepository;

        public RegisterController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]RegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await userRepository.GetUser(user.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError(nameof(user.UserName), "Name already taken.");

                return BadRequest(ModelState);
            }

            await userRepository.AddUser(user.UserName, user.Password);

            return Ok();
        }
    }
}
