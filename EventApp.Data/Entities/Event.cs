using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal EstimatedBudget { get; set; }
        public string Description { get; set; }

        //references Locations Table; (1:1)
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        //references EventTypes Table (1:1)
        public int EventTypeId { get; set; }
        public virtual EventType EventType { get; set; }

        public virtual ICollection<EventGuest> EventGuests { get; set; }
    }
}
