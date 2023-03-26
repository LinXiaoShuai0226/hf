using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using homefinder.Models;

namespace homefinder.ViewModel
{
    public class InsertRentalViewModel
    {
        public Rental rental { get; set; }

        [DisplayName("房屋圖片")]
        [FileExtensions(ErrorMessage = " 所上傳檔案不是圖片 ")]
        public HttpPostedFileBase img1 { get; set; }

        public Members Member { get; set; } = new Members();

    }
}