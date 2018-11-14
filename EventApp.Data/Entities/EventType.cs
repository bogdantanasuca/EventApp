﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Entities
{
    public class EventType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdultsOnly { get; set; }
    }
}
