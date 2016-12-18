using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhereWereWe.Domain.Interfaces;
using WhereWereWe.Domain.Models;
using WhereWereWe.Repositories.Contexts;

namespace WhereWereWe.Repositories
{
    internal class SeriesProgressRepository : ISeriesProgressRepository
    {
        private readonly SeriesProgressContext dbContext;
        private IMapper mapper;

        public SeriesProgressRepository(SeriesProgressContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<SeriesProgress>> GetSeriesProgress(User user)
        {
            var progress = await dbContext.SeriesProgress.ToListAsync();

            return mapper.Map<IEnumerable<SeriesProgress>>(progress);
        }
    }
}
