using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Areas.Admin.Controllers
{
    public class AccountsController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();


        //check account isset
        
        public int CheckAccount(int id)
        {
            int Count = _context.ACCOUNTS.Where(x=>x.ACCOUNTID==id).Count();
            return Count;
        }


        // GET: Admin/Accounts


        public ActionResult Accounts()
        {
            ViewBag.PagePositon = "ACCOUNTS";
            ViewBag.Title = "Danh sách tài khoản - Tài khoản";
            var Accounts = _context.ACCOUNTS.ToList();
            return View(Accounts);
        }


        public ActionResult CreateAccount()
        {
            ViewBag.PagePositon = "ACCOUNTS";
            ViewBag.Title = "Tạo tài khoản - Tài khoản";
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(ACCOUNTS request)
        {
            int Count = _context.ACCOUNTS.Where(x => x.ACCOUNTNAME == request.ACCOUNTNAME).Count();
            if (Count == 0)
            {
                ACCOUNTS NewAccount = new ACCOUNTS()
                {
                    ACCOUNTNAME = request.ACCOUNTNAME,
                    ACCOUNTPASS = request.ACCOUNTPASS,
                    DATECREATE = DateTime.Now,
                    PERMISSON = request.PERMISSON,
                    EMAIL = request.EMAIL,
                };
                _context.ACCOUNTS.Add(NewAccount);
                _context.SaveChanges();
                decimal AccountID = _context.ACCOUNTS.Where( x => x.ACCOUNTNAME.Equals(request.ACCOUNTNAME)).Select( x=> x.ACCOUNTID).FirstOrDefault();
                USERS NewUser = new USERS()
                {
                    FIRSTNAME = "Người",
                    LASTNAME = "Dùng",
                    IMAGENAME = "unknown.png",
                    ACCOUNTID = AccountID,
                };
                _context.USERS.Add(NewUser);
                _context.SaveChanges();

                decimal UserID = (decimal)_context.USERS.Where(x => x.ACCOUNTID == AccountID).Select( x => x.USERID).FirstOrDefault();
                return RedirectToAction("EditUser", "Users", new {@id = UserID });
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditAccount(int id, string message = "")
        {
            ViewBag.PagePositon = "ACCOUNTS";
            ViewBag.Title = "Cập nhật - Tài khoản";
            if(message != "")   ViewBag.Message = message;

            try
            {
                int Count = CheckAccount(id);
                if (Count == 1)
                {
                    var Account = _context.ACCOUNTS.Where(x => x.ACCOUNTID == id).FirstOrDefault();
                    return View(Account);
                }
                else
                {
                    return RedirectToAction("Error", "MainDashboard");
                }
            }catch(Exception)
            {
                return RedirectToAction("Error", "MainDashboard");
            }
        }


        [HttpPost]
        public ActionResult EditAccount(FormCollection fc, ACCOUNTS request)
        {
            var Account = _context.ACCOUNTS.Where(x => x.ACCOUNTID == request.ACCOUNTID).FirstOrDefault();
            Account.ACCOUNTPASS = (fc["ACCOUNTPASS"] == "" ? Account.ACCOUNTPASS : fc["ACCOUNTPASS"]);
            Account.PERMISSON = request.PERMISSON;
            _context.Entry(Account).State = EntityState.Modified;
            _context.SaveChanges();
            string message = "Thông tin đã được cập nhật";
            return RedirectToAction("EditAccount", new { @id = (int)request.ACCOUNTID, @message = message});
        }

        [HttpGet]
        public ActionResult DeleteAccount(int id)
        {
            int Count = _context.ACCOUNTS.Where( x=> x.ACCOUNTID == id).Count();
            if (Count != 0)
            {
                var User = _context.USERS.Where( x => x.USERID== id).FirstOrDefault();
                _context.USERS.Remove(User);
                _context.SaveChanges();

                var Account = _context.ACCOUNTS.Where( x => x.ACCOUNTID.Equals(id)).FirstOrDefault();
                _context.ACCOUNTS.Remove(Account);
                _context.SaveChanges();

                return RedirectToAction("Accounts");
            }
            return RedirectToAction("Error", "MainDashboard");
        }
    }
}