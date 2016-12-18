using System.Collections.Generic;
using System.Threading.Tasks;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Domain.Interfaces
{
    public interface ISeriesProgressRepository
    {
        Task<IEnumerable<SeriesProgress>> GetSeriesProgress(User user);
    }
}
