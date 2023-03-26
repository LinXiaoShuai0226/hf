using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace homefinder.Models
{
    public class Members
    {
        public Guid members_id { get; set; }
        [DisplayName("帳號")]
        [Required(ErrorMessage ="請輸入帳號")]
        [StringLength(30,MinimumLength =6,ErrorMessage ="帳號長度需介於6-30")]
        [Remote("AccountCheck","Members",ErrorMessage ="此帳號已被註冊過了")]
        public string account { get; set; }
        [DisplayName("密碼")]
        public string password { get; set; }
        [DisplayName("姓名")]
        [Required(ErrorMessage = "請輸入姓名")]
        [StringLength(10, ErrorMessage = " 姓名長度最多 10 ")]
        public string name { get; set; }
        [DisplayName("EMAIL")]
        [Required(ErrorMessage = "請輸入EMAIL")]
        [StringLength(300, ErrorMessage = "Email 長度最多 300 ")]
        [EmailAddress(ErrorMessage ="這不是Email格式")]
        public string email { get; set; }
        [DisplayName("電話")]
        [Required(ErrorMessage = "請輸入電話")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "请输入有效的10位電話號碼")]
        public string phone { get; set; }
        //驗證碼
        public string authcode { get; set; }
        [DisplayName("身分")]
        [Required(ErrorMessage = "請選擇身分")]
        public int identity { get; set; }
        [DisplayName("信用分數")]
        public int sorce { get; set; }
    }
}