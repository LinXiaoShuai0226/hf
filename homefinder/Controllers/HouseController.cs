using homefinder.Models;
using homefinder.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using homefinder.ViewModel;

namespace homefinder.Controllers
{
    
    public class HouseController : Controller
    {
        private readonly HouseDBService houseService = new HouseDBService();
        #region 首頁=>感覺要用block囉還有分頁
        public ActionResult Index(HomeViewModel Data)
        {

            return View();
        }
        #endregion
        #region 新增Rental
        //[Authorize(Roles = "publisher")]
        //public ActionResult InsertHouse()
        //{
        //    return PartialView();
        //}
        [Authorize(Roles = "publisher")]
        [HttpPost]
        public ActionResult InsertRental(InsertRentalViewModel newRental)
        {
            //應該有抓到房東8
            newRental.rental.publisher = User.Identity.Name;   

            //房屋照片，那如果有八張呢
            for (int i = 0; i < 8; i++)
            {
                //我在想要用for還是笨方法
            }
            if (newRental.rental.img1 != null)
            {
                string filename = Path.GetFileName(newRental.img1.FileName);
                string Url = Path.Combine(Server.MapPath("~/Upload/"), filename);
                newRental.img1.SaveAs(Url);
                newRental.rental.img1 = filename;
            }
            
            houseService.InsertHouse_Rental(newRental.rental);
            return RedirectToAction("Index", "House", new { Account = User.Identity.Name });
        }
        #endregion

        

        #region 審核房屋
        [Authorize(Roles = "Admin")]
        public ActionResult CheckUpload(Guid Id)
        {
            houseService.CheckUpload(Id);
            return RedirectToAction("Index", "House");
        }
        #endregion



        #region 修改房屋
        public ActionResult UpdateHouse(Guid id,Rental Data)
        {
            //先判斷是否審核，上架
            //才允許修改
            return View();
        }
        #endregion
        #region 刪除房屋
        [Authorize(Roles = "publisher")] 
        public ActionResult Delete(Guid Id)
        {
            houseService.DeleteRental(Id);
            return RedirectToAction("Index");
        }
        #endregion



        #region 
        #endregion
    }
}