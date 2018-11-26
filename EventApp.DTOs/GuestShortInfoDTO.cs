using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.DTOs
{
    public class GuestShortInfoDTO:GuestNameDTO
    {
        public string Email { get; set; }
    }
}
