using AutoMapper;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Repositories.Configuration
{
    public class SeriesMappingProfile : Profile
    {
        public SeriesMappingProfile()
        {
            CreateMap<Series, Entities.Series>().ReverseMap();
        }
    }
}
