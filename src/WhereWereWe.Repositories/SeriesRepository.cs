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
        private SeriesContext _dbContext;
        private IMapper _mapper;

        public SeriesRepository(SeriesContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Series>> GetSeries()
        {
            var series = await _dbContext.Series.ToListAsync();

            return _mapper.Map<IEnumerable<Series>>(series);
        }

        public async Task AddSeries(Series series)
        {
            var seriesEntity = _mapper.Map<Entities.Series>(series);

            _dbContext.Series.Add(seriesEntity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
