using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using homefinder.Models;

namespace homefinder.ViewModel
{
    public class InsertRentalViewModel
    {
        public Rental rental { get; set; }

        public Members Member { get; set; } = new Members();

    }
}