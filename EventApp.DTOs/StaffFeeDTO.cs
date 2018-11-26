using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.DTOs
{
    public class StaffFeeDTO
    {
        public decimal MaxFee { get; set; }
        public decimal MinFee { get; set; }
        public decimal AvgFee { get; set; }
    }
}
