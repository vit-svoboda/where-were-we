using System;

namespace WhereWereWe.Domain.Models
{
    public class Series
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Seasons { get; set; }
        public int EpisodesPerSeason { get; set; }
    }
}
