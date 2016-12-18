using AutoMapper;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Repositories.Configuration
{
    public class SeriesProgressMappingProfile : Profile
    {
        public SeriesProgressMappingProfile()
        {
            CreateMap<SeriesProgress, Entities.SeriesProgress>()
                .ForMember(dst => dst.User, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
