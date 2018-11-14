using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Entities
{
    public class EventGuest
    {
        
        public bool HasAttended { get; set; }
        public bool ConfirmedAttendence { get; set; }

        public virtual int EventId { get; set; }
        public virtual Event Event { get; set; }
                
        public virtual Guest Guest { get; set; }
        public virtual int GuestId { get; set; }

    }
}
