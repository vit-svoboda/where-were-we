using System.Security.Claims;
using System.Threading.Tasks;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(ClaimsPrincipal principal);
    }
}
