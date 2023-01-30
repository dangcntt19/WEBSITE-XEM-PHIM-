using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;
using static System.Net.WebRequestMethods;

namespace WEBSITE_FILM_002.Controllers
{
    public class _UsersController : Controller
    {
        // GET: _Users

        WEBSITE_FILM _context = new WEBSITE_FILM();

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

        public ActionResult UserPage()
        {
            if (Session["isLogin"] != null)
            {
                if ((bool)Session["isLogin"])
                {
                    int userid = Int32.Parse(Session["UserID"].ToString());
                    int account = Int32.Parse(Session["AccountID"].ToString()) ;

                    var _user = _context.USERS.Where(x => x.USERID == userid).FirstOrDefault();
                    var _account = _context.ACCOUNTS.Where(x => x.ACCOUNTID == account).FirstOrDefault();

                    ViewBag._User = _user;
                    ViewBag._Account = _account;

                    return View();
                }
            }
            return RedirectToAction("Login","Pages");
        }

        [HttpPost]
        public ActionResult ChangeAvatar(HttpPostedFileBase image)
        {
            if(CheckIsLogin())
            {
                try
                {
                    string _Filname_Image = Path.GetFileName(image.FileName);
                    string _path_Image = Path.Combine(Server.MapPath("~/Images/Users"), _Filname_Image);

                    int userid = Int32.Parse(Session["UserID"].ToString());
                    var _user = _context.USERS.Where(x => x.USERID == userid).FirstOrDefault();

                    _user.IMAGENAME = _Filname_Image;

                    Session["UserAvatar"] = _Filname_Image;

                    image.SaveAs(_path_Image);
                    _context.Entry(_user).State = EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("UserPage", "_Users");
                }
                catch (Exception)
                {
                    return RedirectToAction("UserPage", "_Users");
                }
            }
            return RedirectToAction("Login", "Pages");
        }

        [HttpPost]
        public ActionResult user_Info(FormCollection formCollection)
        {
            if(CheckIsLogin())
            {
                try
                {
                    int userid = Int32.Parse(Session["UserID"].ToString());
                    string firstname = formCollection["firstname"];
                    string lastname = formCollection["lastname"];
                    DateTime born = DateTime.Parse(formCollection["born"]);
                    int gender = Int32.Parse(formCollection["gender"]);

                    var _user = _context.USERS.Where(x=>x.USERID == userid).FirstOrDefault();

                    if(_user != null)
                    {
                        _user.FIRSTNAME = firstname;
                        _user.LASTNAME = lastname;
                        _user.BORN = (DateTime)born;
                        _user.GENDER = gender;

                        _context.Entry(_user).State = EntityState.Modified;
                        _context.SaveChanges();

                        return RedirectToAction("UserPage", "_Users");
                    }

                }catch (Exception){
                        return RedirectToAction("UserPage", "_User");
                    }
            }
            return RedirectToAction("Login", "Pages");
        }

        [HttpPost]
        public  ActionResult user_account(FormCollection formCollection)
        {
            if(CheckIsLogin())
            {
                int accountid = Int32.Parse(Session["AccountID"].ToString());
                string pass = formCollection["password"].ToString();
                string email = formCollection["email"].ToString();

                var _account = _context.ACCOUNTS.Where(x=>x.ACCOUNTID== accountid).FirstOrDefault();
                if(_account != null)
                {
                    try
                    {
                        _account.EMAIL = email;
                        _account.ACCOUNTPASS = pass;

                        _context.Entry(_account).State = EntityState.Modified;
                        _context.SaveChanges();

                        return RedirectToAction("UserPage", "_Users");

                    }
                    catch (Exception)
                    {
                        return RedirectToAction("UserPage", "_Users");
                    }
                }
            }
            return RedirectToAction("Login", "Pages");
        }

    }
}