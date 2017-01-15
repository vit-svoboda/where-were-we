using System;
using WhereWereWe.Domain.Models;

namespace WhereWereWe.Api.Models
{
    public class ProgressViewModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public int Season { get; set; }

        public int Seasons { get; set; }

        public int Episode { get; set; }

        public int Episodes { get; set; }

        public ProgressViewModel(SeriesProgress progress)
        {
            if (progress?.Series == null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            Id = progress.Series.Id;
            Name = progress.Series.Name;
            Season = progress.Season;
            Seasons = progress.Series.Seasons;
            Episode = progress.Episode;
            Episodes = progress.Series.EpisodesPerSeason;
        }
    }
}
