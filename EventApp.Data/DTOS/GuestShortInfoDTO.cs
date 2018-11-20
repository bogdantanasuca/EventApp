using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.DTOS
{
    public class GuestShortInfoDTO:GuestNameDTO
    {
        public string Email { get; set; }
    }
}
