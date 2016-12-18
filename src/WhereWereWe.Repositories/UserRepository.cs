using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WhereWereWe.Domain.Interfaces;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly UserContext dbContext;
        private readonly IMapper mapper;

        public UserRepository(UserContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task AddUser(string userName, string password)
        {
            var user = new Entities.User
            {
                Name = userName,
            };

            var hasher = new PasswordHasher<Entities.User>();
            user.PasswordHash = hasher.HashPassword(user, password);

            await dbContext.AddAsync(user);

            await dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUser(string userName)
        {
            var userEntity = await GetUserEntity(userName);

            return mapper.Map<User>(userEntity);
        }

        public async Task<User> ValidateLogin(string userName, string password)
        {
            var user = await GetUserEntity(userName);
            if (user == null)
            {
                return null;
            }

            var hasher = new PasswordHasher<Entities.User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            switch (result)
            {
                case PasswordVerificationResult.Success:
                    break;

                case PasswordVerificationResult.SuccessRehashNeeded:
                    user.PasswordHash = hasher.HashPassword(user, password);
                    dbContext.Users.Update(user);

                    await dbContext.SaveChangesAsync();
                    break;

                default:
                    return null;
            }

            return mapper.Map<User>(user);
        }

        private async Task<Entities.User> GetUserEntity(string userName)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Name == userName);
        }
    }
}
