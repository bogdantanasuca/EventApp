﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.ClassTemplates
{
    public class LocationTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public short Capacity { get; set; }
        public decimal RentFee { get; set; }
    }
}
