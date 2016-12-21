using System;

namespace WhereWereWe.Api.Models
{
    public class ProgressUpdateViewModel
    {
        public Guid Id { get; set; }

        public int Episode { get; set; }

        public int Season { get; set; }
    }
}
