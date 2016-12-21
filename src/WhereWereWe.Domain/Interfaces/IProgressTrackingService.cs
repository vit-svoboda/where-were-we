using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Domain.Interfaces
{
    public interface IProgressTrackingService
    {
        Task<IEnumerable<SeriesProgress>> GetAllProgress(User viewer);

        Task<SeriesProgress> StartProgress(User viewer, Guid seriesId);

        Task UpdateProgress(User viewer, SeriesProgress progress);
    }
}
