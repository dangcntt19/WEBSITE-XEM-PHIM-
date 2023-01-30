using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();

        // GET: Admin/Admin

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection formCollection)
        {
            string username = formCollection["username"];
            string password = formCollection["password"];
            var Account = _context.ACCOUNTS.Where(x=>x.ACCOUNTNAME.Equals(username)).FirstOrDefault();

            if (Account != null)
            {
                bool checkPass = Account.ACCOUNTPASS.ToString().Equals(password);
                if (checkPass)
                {
                    int Permisson = Account.PERMISSON;

                    var User = _context.USERS.Where(x=>x.ACCOUNTID.Equals(Account.ACCOUNTID)).FirstOrDefault();

                    int userid = (int)User.USERID;
                    string FirstName = User.FIRSTNAME;
                    string LastName = User.LASTNAME;
                    string UserAvatar = User.IMAGENAME;


                    Session["isLogin"] = true;
                    Session["AccountID"] = Account.ACCOUNTID;
                    Session["AccountName"] = username;
                    Session["Permisson"] = Permisson;
                    Session["UserID"] = userid;
                    Session["FirstName"] = FirstName;
                    Session["LastName"] = LastName;
                    Session["UserAvatar"] = UserAvatar;

                    return RedirectToAction("index", "MainDashboard");
                }
                else
                {
                    ViewBag.Message = "Sai mật khẩu";
                }
            }
            else
            {
                ViewBag.Message = "Sai tài khoản đăng nhập";
            }
            ViewBag.username = username;
            ViewBag.password = password;
            return View();
        }

        public ActionResult Logout()
        {
            Session["isLogin"] = false;
            Session.Remove("AccountID");
            Session.Remove("AccountName");
            Session.Remove("Permisson");
            Session.Remove("UserID");
            Session.Remove("FirstName");
            Session.Remove("LastName");
            Session.Remove("UserAvatar");
            return RedirectToAction("Login", "Admin");
        }
    }
}