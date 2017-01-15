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

        public async Task<Series> AddSeries(Series series)
        {
            var seriesEntity = mapper.Map<Entities.Series>(series);

            var existing = await dbContext.Series.FirstOrDefaultAsync(s => s.Name == series.Name);
            if (existing == null)
            {
                dbContext.Series.Add(seriesEntity);

                await dbContext.SaveChangesAsync();
            }

            return mapper.Map<Series>(existing ?? seriesEntity);
        }

        public async Task<Series> GetSeries(Guid id)
        {
            var series = await dbContext.Series.FirstAsync(s => s.Id == id);

            return mapper.Map<Series>(series);
        }
    }
}
