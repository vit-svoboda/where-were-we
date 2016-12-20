using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhereWereWe.Domain.Interfaces;
using WhereWereWe.Domain.Models;
using WhereWereWe.Repositories.Contexts;
using System;

namespace WhereWereWe.Repositories
{
    internal class SeriesRepository : ISeriesRepository
    {
        private SeriesContext dbContext;
        private IMapper mapper;

        public SeriesRepository(SeriesContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Series>> GetSeries()
        {
            var series = await dbContext.Series.ToListAsync();

            return mapper.Map<IEnumerable<Series>>(series);
        }

        public async Task AddSeries(Series series)
        {
            var seriesEntity = mapper.Map<Entities.Series>(series);

            dbContext.Series.Add(seriesEntity);

            await dbContext.SaveChangesAsync();
        }

        public async Task<Series> GetSeries(Guid id)
        {
            var series = await dbContext.Series.FirstAsync(s => s.Id == id);

            return mapper.Map<Series>(series);
        }
    }
}
