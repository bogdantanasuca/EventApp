using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.DTOs
{
    public class EventGuestDTO
    {
        public bool HasAttended { get; set; }
        public bool ConfirmedAttendence { get; set; }
        public virtual int EventId { get; set; }
        public virtual int GuestId { get; set; }
        public decimal? GiftAmount { get; set; }
    }
}
