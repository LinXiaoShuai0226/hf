using homefinder.Models;
using homefinder.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace homefinder.Controllers
{
    
    public class HouseController : Controller
    {
        private readonly HouseDBService houseService = new HouseDBService();
        private readonly MembersDBService membersService = new MembersDBService();
        #region 首頁
        public ActionResult Index()
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
        public ActionResult InsertEquipment(Equipment newEquipment)
        {
            houseService.InsertHouse_Equipment(newEquipment);
            return RedirectToAction("Index", "House");
        }
        #endregion

        #region 審核保存房屋
        #endregion



        #region 修改房屋
        #endregion
        #region 刪除房屋
        #endregion



        #region 
        #endregion
    }
}