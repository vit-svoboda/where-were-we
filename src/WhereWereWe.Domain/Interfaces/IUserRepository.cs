using System.Threading.Tasks;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string userName);

        Task<User> ValidateLogin(string userName, string password);

        Task AddUser(string userName, string password);
    }
}
