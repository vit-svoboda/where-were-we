using System.Collections.Generic;
using System.Threading.Tasks;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Domain.Interfaces
{
    public interface ISeriesRepository
    {
        Task<IEnumerable<Series>> GetSeries();

        Task AddSeries(Series series);
    }
}