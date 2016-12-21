using System;
using System.ComponentModel.DataAnnotations;

namespace WhereWereWe.Repositories.Entities
{
    internal class SeriesProgress
    {
        public Guid Id { get; set; }

        [Required]
        public Series Series { get; set; }

        [Required]
        public User User { get; set; }

        public int Season { get; set; }

        public int Episode { get; set; }
    }
}
