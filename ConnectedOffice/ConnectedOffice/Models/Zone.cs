using System;
using System.Collections.Generic;

namespace ConnectedOffice.Models
{
    public partial class Zone
    {
        public Guid ZoneId { get; set; }
        public string ZoneName { get; set; } = null!;
        public string? ZoneDescription { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
