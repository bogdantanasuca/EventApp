using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Entities
{

    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public short Capacity { get; set; }
        public decimal RentFee { get; set; }
        //(1:n) one location has multiple events
        public virtual ICollection<Event> Events { get; set; }
        //(1:n) one location has people as staffs
        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
