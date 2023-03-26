using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homefinder.Models
{
    public class Rental
    {
        public Guid rental_id { get; set; }
        public string publisher { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string address { get; set; }
        public float area { get; set; }
        public int rent { get; set; }
        public string type { get; set; }
        public int adminfee { get; set; }
        public int waterfee { get; set; }
        public int electricitybill { get; set; }
        public bool check { get; set; }
        public bool tenant { get; set; }
        public DateTime uploadtime { get; set; }
        public Members Member { get; set; } = new Members();
    }
}