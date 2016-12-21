using System.Collections.Generic;
using System.Threading.Tasks;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Domain.Interfaces
{
    public interface ISeriesProgressRepository
    {
        Task<IEnumerable<SeriesProgress>> GetSeriesProgress(User user);

        Task<SeriesProgress> AddSeriesProgress(User user, Series series);

        Task<SeriesProgress> UpdateSeriesProgress(User user, SeriesProgress seriesProgress);
    }
}
