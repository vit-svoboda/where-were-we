using System;

namespace WhereWereWe.Domain.Models
{
    public class SeriesProgress
    {
        public Guid Id { get; set; }

        public Series Series { get; set; }

        public int Season { get; set; }

        public int Episode { get; set; }
    }
}