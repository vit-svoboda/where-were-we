using System.Security.Claims;
using System.Threading.Tasks;
using WhereWereWe.Domain.Interfaces;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public async Task<User> GetUser(ClaimsPrincipal principal)
        {
            var userName = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await userRepository.GetUser(userName);
        }
    }
}
