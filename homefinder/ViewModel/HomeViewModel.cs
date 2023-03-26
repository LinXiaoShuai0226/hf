using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using homefinder.Models;

namespace homefinder.ViewModel
{
    public class HomeViewModel
    {
        public Rental rental { get; set; }
        public Equipment equipment { get; set; }
        public bool isCheck { get; set; }
    }
}