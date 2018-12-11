using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Web.Models.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public string Description { get; set; }
        public decimal EstimatedBudget { get; set; }
        public int EventTypeId { get; set; }
        public int LocationId { get; set; }
    }
}
