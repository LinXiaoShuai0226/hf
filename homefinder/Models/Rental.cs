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
        public string publisher { get; set; }
        [DisplayName("房屋標題")]
        public string title { get; set; }
        [DisplayName("房屋介紹")]
        public string content { get; set; }
        [DisplayName("地址")]
        public string address { get; set; }
        [DisplayName("面積")]
        public float area { get; set; }
        [DisplayName("租金")]
        public int rent { get; set; }
        [DisplayName("類型")]
        public string type { get; set; }
        [DisplayName("管理費")]
        public int adminfee { get; set; }
        [DisplayName("水費")]
        public int waterfee { get; set; }
        [DisplayName("電費")]
        public int electricitybill { get; set; }
        [DisplayName("是否審核")]
        public bool check { get; set; }
        [DisplayName("是否承租")]
        public bool tenant { get; set; }
        [DisplayName("上傳時間")]
        public DateTime uploadtime { get; set; }

        public Members Member { get; set; } = new Members();
    }
}