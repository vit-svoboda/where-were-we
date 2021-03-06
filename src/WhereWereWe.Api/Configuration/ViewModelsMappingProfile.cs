﻿using AutoMapper;
using WhereWereWe.Api.Models;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Api.Configuration
{
    public class ViewModelsMappingProfile : Profile
    {
        public ViewModelsMappingProfile()
        {
            CreateMap<NewSeriesViewModel, Series>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ProgressUpdateViewModel, SeriesProgress>()
                .ForMember(dst => dst.Series, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
