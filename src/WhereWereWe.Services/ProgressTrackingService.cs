using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhereWereWe.Domain.Interfaces;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Services
{
    internal class ProgressTrackingService : IProgressTrackingService
    {
        private readonly ISeriesProgressRepository progressRepository;
        private readonly ISeriesRepository seriesRepository;

        public ProgressTrackingService(ISeriesProgressRepository progressRepository, ISeriesRepository seriesRepository)
        {
            this.progressRepository = progressRepository;
            this.seriesRepository = seriesRepository;
        }

        public async Task<IEnumerable<SeriesProgress>> GetAllProgress(User viewer)
        {
            return await progressRepository.GetSeriesProgress(viewer);
        }

        public async Task<SeriesProgress> StartProgress(User viewer, Guid seriesId)
        {
            var series = await seriesRepository.GetSeries(seriesId);

            return await progressRepository.AddSeriesProgress(viewer, series);
        }

        public async Task<SeriesProgress> UpdateProgress(User viewer, SeriesProgress progress)
        {
            return await progressRepository.UpdateSeriesProgress(viewer, progress);
        }
    }
}
