using AutoMapper;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Repositories.Configuration
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, Entities.User>()
                .ForMember(dst => dst.PasswordHash, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
