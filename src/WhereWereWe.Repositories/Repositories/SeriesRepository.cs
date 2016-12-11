using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhereWereWe.Domain.Interfaces;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Repositories
{
    public class SeriesRepository : ISeriesRepository
    {
        private SeriesContext dbContext;
        private IMapper mapper;

        internal SeriesRepository(SeriesContext dbContext, IMapper mapper)
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
    }
}
