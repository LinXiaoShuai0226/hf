using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homefinder.Models
{
    public class Equipment
    {
        public Guid equipment_id { get; set; }
        public Guid rental_id { get; set; }
        public string equipmentname { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }
        public string img4 { get; set; }
        public string img5 { get; set; }
        public string img6 { get; set; }
        public string img7 { get; set; }
        public string img8 { get; set; }
        public Rental Rental { get; set; } = new Rental();
    }
}