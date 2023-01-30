using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Controllers
{
    public class PagesController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();

        [HttpGet]
        public ActionResult Detail_Film(int id)
        {
            var _film = _context.FILMS.Where(x=>x.FILMID == id).FirstOrDefault();

            //Lấy danh sách bình luận của phim

            var _comment = (from cmt in _context.COMMENTS
                            join user in _context.USERS on cmt.USERID equals user.USERID
                            where cmt.COMMENT_STATUS == 0 && cmt.FILMID == id
                            select new _ListComments
                            {
                                comment_content = cmt.COMMENT_CONTENT.ToString(),
                                date = cmt.DATECREATE,
                                userFirstName = user.FIRSTNAME,
                                userLastName = user.LASTNAME,
                                avatar = user.IMAGENAME,
                            }).ToList();

            ViewBag.Comment = _comment;


            // Lấy danh sách phim mới

            var _NewFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.STATUS != null).OrderByDescending(x => (DateTime)x.PRODUCTIONYEAR).Take(8);

            ViewBag.NewFilm = _NewFilm;

            // Xếp hạng danh sách phim

            var _RankFilm = _context.FILMS.Where(x=>x.FILM_STATUS == 0).OrderByDescending(x=>x.VIEWS).ThenByDescending(x=>x.FILMID).Take(5);

            ViewBag.RankFilm = _RankFilm;

            return View(_film) ;
        }

        //trang chủ 

        public ActionResult Index()
        {
            // danh sách phim mới
            var _NewFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0).OrderByDescending(x => x.FILMID).Take(12).ToList();
            ViewBag.NewFilm = _NewFilm;

            // danh sách phim theo chủ đề
            var _ShowingFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.STATUS == "Đang chiếu").OrderByDescending(x => x.FILMID).Take(12).ToList();
            ViewBag.ShowingFilm = _ShowingFilm;

            // danh sách phim theo ngôn ngữ
            var _LanguageTQFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.LANGUAGE == "Trung Quốc").OrderByDescending(x => x.FILMID).Take(12).ToList();
            var _LanguageTVFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.LANGUAGE == "Phụ đề Việt").OrderByDescending(x => x.FILMID).Take(12).ToList();
            var _LanguageHQFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.LANGUAGE == "Hàn Quốc").OrderByDescending(x => x.FILMID).Take(12).ToList();
            
            ViewBag.TQFilm = _LanguageTQFilm;
            ViewBag.TVFilm = _LanguageTVFilm;
            ViewBag.HQFilm = _LanguageHQFilm;
            
            // Xếp hạng phim

            var _RankFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0).OrderByDescending(x => x.VIEWS).ThenByDescending(x => x.FILMID).Take(3);
            ViewBag.RankFilm = _RankFilm;


            return View();
        }

        // Trang xem phim
        public ActionResult PlayVideo(int id)
        {
            // Lấy Film ra chiếu
            var _Film = _context.FILMS.Where(x=>x.FILMID == id && x.FILM_STATUS == 0).SingleOrDefault();

            // Lấy danh sách phim mới

            var _NewFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0 && x.STATUS != null).OrderByDescending(x => (DateTime)x.PRODUCTIONYEAR).Take(8);

            ViewBag.NewFilm = _NewFilm;

            // Xếp hạng danh sách phim

            var _RankFilm = _context.FILMS.Where(x => x.FILM_STATUS == 0).OrderByDescending(x => x.VIEWS).ThenByDescending(x => x.FILMID).Take(5);

            ViewBag.RankFilm = _RankFilm;

            //Lấy danh sách bình luận của phim

            var _comment = (from cmt in _context.COMMENTS
                            join user in _context.USERS on cmt.USERID equals user.USERID
                            where cmt.COMMENT_STATUS == 0 && cmt.FILMID == id
                            select new _ListComments
                            {
                                comment_content = cmt.COMMENT_CONTENT.ToString(),
                                date = cmt.DATECREATE,
                                userFirstName = user.FIRSTNAME,
                                userLastName = user.LASTNAME,
                                avatar = user.IMAGENAME,
                            }).ToList();

            ViewBag.Comment = _comment;

            return View(_Film);
        }

        // Trang đăng nhập

        public ActionResult Login()
        {
            if (Session["isLogin"] != null)
            {
                if ((bool)Session["isLogin"])
                {
                    return RedirectToAction("UserPage", "_Users");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login (FormCollection formCollection)
        {
            string username = formCollection["ACCOUNTNAME"];
            string userpass = formCollection["ACCOUNTPASS"];
            var _account = _context.ACCOUNTS.Where(x=> x.ACCOUNTNAME == username).FirstOrDefault();
            if (_account != null)
            {
                bool checkpass = _account.ACCOUNTPASS.Equals(userpass);
                if(checkpass)
                {
                    var _user = _context.USERS.Where(x => x.ACCOUNTID == _account.ACCOUNTID).FirstOrDefault();
                    if (_user != null)
                    {

                        Session["isLogin"] = true;
                        Session["AccountID"] = _account.ACCOUNTID;
                        Session["AccountName"] = username;
                        Session["Permisson"] = _account.PERMISSON;
                        Session["UserID"] = _user.USERID;
                        Session["FirstName"] = _user.FIRSTNAME;
                        Session["LastName"] = _user.LASTNAME;
                        Session["UserAvatar"] = _user.IMAGENAME;

                        return RedirectToAction("UserPage", "_Users");
                    }
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            if (Session["isLogin"] != null)
            {
                if ((bool)Session["isLogin"])
                {
                    return RedirectToAction("UserPage", "_Users");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection formCollection, HttpPostedFileBase image)
        {

            try
            {
                string _Filname_Image = Path.GetFileName(image.FileName);
                string _path_Image = Path.Combine(Server.MapPath("~/Images/Users"), _Filname_Image);


                string firstname = formCollection["firstname"];
                string lastname = formCollection["lastname"];
                DateTime born = DateTime.Parse(formCollection["born"]);

                string username = formCollection["username"];
                string password = formCollection["userpass"];


                int CountAccount = _context.ACCOUNTS.Where(x => x.ACCOUNTNAME == username).Count();

                if(CountAccount == 0)
                {
                    ACCOUNTS _account = new ACCOUNTS()
                    {
                        ACCOUNTNAME = username,
                        ACCOUNTPASS = password,
                        DATECREATE = DateTime.Now,
                    };

                    USERS _user = new USERS()
                    {
                        FIRSTNAME = firstname,
                        LASTNAME = lastname,
                        BORN = born,
                        IMAGENAME = _Filname_Image,
                    };

                    _context.ACCOUNTS.Add(_account);
                    _context.USERS.Add(_user);

                    image.SaveAs(_path_Image);
                    _context.SaveChanges();

                    return RedirectToAction("Login");
                }
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }


        public ActionResult Logout()
        {
            Session.Remove("isLogin");
            Session.Remove("AccountID");
            Session.Remove("AccountName");
            Session.Remove("Permisson");
            Session.Remove("UserID");
            Session.Remove("FirstName");
            Session.Remove("LastName");
            Session.Remove("UserAvatar");

            return RedirectToAction("Index", "Pages");
        }

        //danh sách phim tỳ ý
        public ActionResult ListFilm(string category = "", string language = "", int page = 1, int pageSize = 18)
        {
            if(category != "")
            {
                var _film = _context.FILMS.Where(x => x.CATEGORY == category).OrderByDescending(x => x.FILMID).ThenByDescending(x => x.MOVIEREVIEW).ToPagedList(page, pageSize);
                ViewBag.category = category;
                return View(_film);
            }
            else if(language != "")
            {
                var _film = _context.FILMS.Where(x => x.LANGUAGE == language).OrderByDescending(x => x.FILMID).ThenByDescending(x => x.MOVIEREVIEW).ToPagedList(page, pageSize);
                ViewBag.language = language;
                return View(_film);
            }
            else
            {
                var _film = _context.FILMS.OrderByDescending(x => x.FILMID).ToPagedList(page, pageSize);
                ViewBag.listfilm = "Danh sách phim";
                return View(_film);
            }
            //return RedirectToAction("Index", "Pages");
        }


        //test
        public JsonResult GET_FILM()
        {
            var _Film = (from f in _context.FILMS
                         where f.FILM_STATUS == 0 && f.LANGUAGE == "Phụ đề Việt"
                         select new
                         {
                             FILMID = f.FILMID,
                         }).OrderByDescending(x=>x.FILMID).Take(24).ToList();
            return Json(JsonConvert.SerializeObject(_Film), JsonRequestBehavior.AllowGet);
        }
    }
}