using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homefinder.Models
{
    public class BookList
    {
        public Guid booklist_id { get; set; }
        public string lessee { get; set; }
        public Guid rental_id { get; set; }
        public Guid booktime_id { get; set; }
        public Members Member { get; set; } = new Members();
        public Rental Rental { get; set; } = new Rental();
        public BookTime BookTime { get; set; } = new BookTime();
    }
}