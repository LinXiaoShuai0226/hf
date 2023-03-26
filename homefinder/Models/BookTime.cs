using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homefinder.Models
{
    public class BookTime
    {
        public Guid booktime_id { get; set; }
        public string publisher { get; set; }
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }
        public Members Member { get; set; } = new Members();
    }
}