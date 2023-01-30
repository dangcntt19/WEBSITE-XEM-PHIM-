using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        WEBSITE_FILM _conntext = new WEBSITE_FILM();

        public bool CheckIsLogin()
        {
            if (Session["isLogin"] != null)
            {
                if ((bool)Session["isLogin"])
                {
                    return true;
                }
            }
            return false;
        }

        // GET: Admin/Users
        public ActionResult Users()
        {
            ViewBag.PagePositon = "USERS";
            var Users = _conntext.USERS.ToList();
            return View(Users);
        }

        [HttpGet]
        public ActionResult EditUser (int id)
        {
            ViewBag.PagePositon = "USERS";
            var user  = _conntext.USERS.Where( x => x.USERID == id ).FirstOrDefault(); 
            if(user != null)
            {
                return View(user);
            }
            return RedirectToAction("Error", "MainDashboarh");
        }

        [HttpPost]
        public ActionResult EditUser(USERS request)
        {
            var User = _conntext.USERS.Where( x => x.USERID == request.USERID).FirstOrDefault();
            if(User != null)
            {
                User.LASTNAME = request.LASTNAME;
                User.FIRSTNAME = request.FIRSTNAME;
                User.BORN = (DateTime)request.BORN;
                User.GENDER = request.GENDER;
                //User.IMAGENAME = Session["UserAvatar"].ToString();
                User .USERSTATUS = request.USERSTATUS;
                _conntext.Entry(User).State = EntityState.Modified;
                _conntext.SaveChanges();

                return View(User);
            }
            return RedirectToAction("Error", "MainDashboard");
        }


        [HttpPost]
        public ActionResult ChangeAvatar(HttpPostedFileBase image)
        {
            if (CheckIsLogin())
            {
               
                try
                {
                    string _Filname_Image = Path.GetFileName(image.FileName);
                    string _path_Image = Path.Combine(Server.MapPath("~/Images/Users"), _Filname_Image);

                    int userid = Int32.Parse(Session["UserID"].ToString());
                    var _user = _conntext.USERS.Where(x => x.USERID == userid).FirstOrDefault();

                    _user.IMAGENAME = _Filname_Image;

                    Session["UserAvatar"] = _Filname_Image;

                    image.SaveAs(_path_Image);
                    _conntext.Entry(_user).State = EntityState.Modified;
                    _conntext.SaveChanges();

                    return RedirectToAction("EditUser", "Users", new {id = _user.USERID});
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "MainDashboard");
                }
            }
            return RedirectToAction("Login", "Admin");
        }

        [HttpGet]
        public ActionResult DetailUser(int id)
        {
            ViewBag.PagePositon = "USERS";
            var User = _conntext.USERS.Where( x => x.USERID == id ).FirstOrDefault();
            if(User != null )
            {
                return View(User);
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            int Count = _conntext.USERS.Where(x=>x.USERID == id).Count(); 
            if(Count > 0)
            {
                var User = _conntext.USERS.Where(x=>x.USERID.Equals(id)).FirstOrDefault();
                var Account = _conntext.ACCOUNTS.Where(x=>x.ACCOUNTID.Equals(User.ACCOUNTID)).FirstOrDefault();
                if (Account != null)
                {
                    _conntext.ACCOUNTS.Remove(Account);
                    _conntext.USERS.Remove(User);
                    _conntext.SaveChanges();
                    return RedirectToAction("Users");
                }
            }
            return RedirectToAction("Error", "MainDashboard");
        }

    }
}