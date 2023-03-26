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
        private readonly MembersDBService membersService = new MembersDBService();
        #region 首頁=>感覺要用block囉還有分頁
        public ActionResult Index(HomeViewModel Data)
        {

            return View();
        }
        #endregion
        #region 新增Rental
        //[Authorize(Roles = "lessor")]
        //public ActionResult InsertHouse()
        //{
        //    return PartialView();
        //}
        [Authorize(Roles = "lessor")]
        [HttpPost]
        public ActionResult InsertRental(Rental newRental)
        {
            Members Data = new Members();
            Data.account = User.Identity.Name;
            newRental.Member = membersService.GetDataByAccount(Data.account);
            houseService.InsertHouse_Rental(newRental);
            return RedirectToAction("Index","House");
        }
        #endregion

        #region 新增Equipment
        [Authorize(Roles = "lessor")]
        [HttpPost]
        public ActionResult InsertEquipment(InsertEquipmentViewModel newEquipment)
        {
            //房屋照片，那如果有八張呢
            for (int i = 0; i < 8; i++)
            {
                //我在想要用for還是笨方法
            }
            if (newEquipment.Data.img1!=null)
            {
                string filename = Path.GetFileName(newEquipment.img1.FileName);
                string Url = Path.Combine(Server.MapPath("~/Upload/"), filename);
                newEquipment.img1.SaveAs(Url);
                newEquipment.Data.img1 = filename;
            }
            houseService.InsertHouse_Equipment(newEquipment.Data);
            return RedirectToAction("Index", "House");
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
        #endregion
        #region 刪除房屋
        [Authorize(Roles = "lessor")] 
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