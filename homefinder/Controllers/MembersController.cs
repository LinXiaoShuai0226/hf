using homefinder.Security;
using homefinder.Service;
using homefinder.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace homefinder.Controllers
{
    public class MembersController : Controller
    {
        private readonly MembersDBService membersSerivce = new MembersDBService();
        private readonly MailService mailService = new MailService();

        #region 註冊
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "House");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Register(MembersRegisterViewModel RegisterMember)
        {
            if (ModelState.IsValid)
            {
                RegisterMember.newMember.password = RegisterMember.password;
                string AuthCode = mailService.GetValidateCode();
                RegisterMember.newMember.authcode = AuthCode;
                membersSerivce.Register(RegisterMember.newMember);
                string TempMail = System.IO.File.ReadAllText(Server.MapPath("~/Views/Shared/RegisterEmail.html"));
                UriBuilder ValidateUrl = new UriBuilder(Request.Url)
                {
                    Path = Url.Action("EmailValidate", "Members", new { Account = RegisterMember.newMember.account, AuthCode = RegisterMember.newMember.authcode })
                };
                string MailBody = mailService.GetRegisterMailBody(TempMail, RegisterMember.newMember.name, ValidateUrl.ToString().Replace("%3F", "?"));
                mailService.SendRegisterMail(MailBody, RegisterMember.newMember.email);
                TempData["RegisterState"] = "註冊成功，請去收信以來驗證EMAIL";
                return RedirectToAction("RegisterResult");
            }
            RegisterMember.password = null;
            RegisterMember.passwordCheck = null;
            return View(RegisterMember);
        }
        #endregion
        #region 註冊結果畫面
        public ActionResult RegisterResult()
        {
            return View();
        }
        #endregion
        #region 帳號判斷是重複
        public JsonResult AccountCheck(MembersRegisterViewModel RegisterMember)
        {
            return Json(membersSerivce.AccountCheck(RegisterMember.newMember.account), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 寄驗證信
        public ActionResult EmailValidate(string Account,string AuthCode)
        {
            ViewData["EmailValidate"] = membersSerivce.EmailValidate(Account, AuthCode);
            return View();
        }
        #endregion


        #region 登入
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "house");
            }
            return View();
        }
        public ActionResult Login(MembersLoginViewModel LoginMember)
        {
            string ValidateStr = membersSerivce.LoginCheck(LoginMember.Account, LoginMember.Password);
            if (String.IsNullOrWhiteSpace(ValidateStr))
            {
                string RoleData = membersSerivce.GetRole(LoginMember.Account);
                JwtService jewService = new JwtService();
                string cookieName = WebConfigurationManager.AppSettings["CookieName"].ToString();
                string Token = jewService.GenerateToken(LoginMember.Account, RoleData);
                //cookieName
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Value = Server.UrlEncode(Token);
                Response.Cookies.Add(cookie);
                Response.Cookies[cookieName].Expires = DateTime.Now.AddMinutes(Convert.ToInt32(WebConfigurationManager.AppSettings["ExpireMinutes"]));
                return RedirectToAction("Index", "House");
            }
            else
            {
                ModelState.AddModelError("", ValidateStr);
                return View(LoginMember);
            }
        }
        #endregion
        #region 登出
        [Authorize]
        public ActionResult Logout()
        {
            string cookieName = WebConfigurationManager.AppSettings["CookieName"].ToString();
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = DateTime.Now.AddDays(-1);
            cookie.Values.Clear();
            Response.Cookies.Set(cookie);
            return RedirectToAction("Login");
        }
        #endregion
        #region 修改密碼
        public ActionResult ChangePassword(ChangePasswordViewModel ChangeData)
        {
            if (ModelState.IsValid)
            {
                ViewData["ChangeState"] = membersSerivce.ChangePassword(User.Identity.Name, ChangeData.Password, ChangeData.newPassword);
            }
            return View();
        }
        #endregion
        #region 忘記密碼
        #endregion
    }
}