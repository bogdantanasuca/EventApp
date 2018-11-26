using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.DTOs
{
    public class StaffDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Fee { get; set; }
        //references Locations Table; (1:1)
        public int? LocationId { get; set; }
        //references StaffRole Table; (1:1)
        public int StaffRoleId { get; set; }
    }
}
