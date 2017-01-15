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
    internal class SeriesProgressRepository : ISeriesProgressRepository
    {
        private readonly SeriesProgressContext dbContext;
        private IMapper mapper;

        public SeriesProgressRepository(SeriesProgressContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<SeriesProgress> AddSeriesProgress(User user, Series progress)
        {
            var userEntity = await dbContext.Users.FindAsync(user.Id);
            var seriesEntity = await dbContext.Series.FindAsync(progress.Id);

            var result = await dbContext.SeriesProgress.AddAsync(new Entities.SeriesProgress
            {
                User = userEntity,
                Series = seriesEntity,
                Episode = 1,
                Season = 1
            });

            await dbContext.SaveChangesAsync();

            return mapper.Map<SeriesProgress>(result.Entity);
        }

        public async Task<IEnumerable<SeriesProgress>> GetSeriesProgress(User user)
        {
            var progress = await dbContext.SeriesProgress
                .Include(p => p.Series)
                .ToListAsync();

            return mapper.Map<IEnumerable<SeriesProgress>>(progress);
        }

        public async Task<SeriesProgress> UpdateSeriesProgress(User user, SeriesProgress seriesProgress)
        {
            var progress = await dbContext.SeriesProgress
                .Include(p => p.User)
                .Include(p => p.Series)
                .FirstOrDefaultAsync(p => p.Series.Id == seriesProgress.Id && p.User.Id == user.Id);

            if (progress == null)
            {
                return null;
            }

            progress.Episode = seriesProgress.Episode;
            progress.Season = seriesProgress.Season;

            await dbContext.SaveChangesAsync();

            return mapper.Map<SeriesProgress>(progress);
        }
    }
}
