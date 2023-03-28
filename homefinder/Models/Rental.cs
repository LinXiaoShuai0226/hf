using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace homefinder.Models
{
    public class Rental
    {
        [DisplayName("房屋編號")]
        public Guid rental_id { get; set; }
        [DisplayName("房東")]
        public Guid publisher { get; set; }
        public string genre { get;set; }
        public string pattern { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string address { get; set; }
        public int rent { get; set; }
        public float waterfee { get; set; }
        public float electricitybill { get; set; }
        public int adminfee { get; set; }
        public string floor { get; set; }
        public float area { get; set; }
        
        public string equipmentname { get; set; }
        public string content { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }
        public string img4 { get; set; }
        public string img5 { get; set; }
        public string img6 { get; set; }
        public string img7 { get; set; }
        public string img8 { get; set; }
        public bool check { get; set; }
        public bool tenant { get; set; }
        public DateTime uploadtime { get; set; }


        public Members Member { get; set; } = new Members();
    }
}