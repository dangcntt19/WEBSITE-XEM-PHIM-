using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Areas.Admin.Controllers
{
    public class MainDashboardController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();

        // GET: Admin/MainDashboard
        public ActionResult Index()
        {
            ViewBag.PagePositon = "MAINDASHBOARD";

            // Số lượng tài khoản người dùng

            int CountTaiKhoan = _context.ACCOUNTS.Where(x => x.PERMISSON == 0).Count();

            // Số lượng phim
           
            int CountFilm = _context.FILMS.Where(x=>x.FILM_STATUS == 0).Count();


            //Số lượng người dùng

            int CountUser = _context.USERS.Where(x=>x.USERSTATUS == 0).Count(); 


            // Số lượng tệp tệp đã được tải lên

            int CountFile = _context.IMAGES.Count() + _context.VIDEOS.Count();

            ViewBag.CountFile = CountFile;
            ViewBag.CountFilm = CountFilm;
            ViewBag.CountUser = CountUser;
            ViewBag.CountTaiKhoan = CountTaiKhoan;

            // Rank
            // Người đăng film nhiều nhất

            //var ListUserTopPost = from p in _context.FILMS
            //                      group p by p.USERID into g
            //                      select new
            //                      {
            //                          name = g.Key,
            //                          count = g.Count()
            //                      };

            // Danh sách phim lượt xem nhiều 

            var ListFilmTopView = _context.FILMS.Where(x=>x.FILM_STATUS == 0).OrderByDescending(x=>x.VIEWS).Take(5).ToList();


            // danh sách phim điểm cao nhất
            var ListFilmTopMark= _context.FILMS.Where(x => x.FILM_STATUS == 0).OrderByDescending(x => x.MOVIEREVIEW).Take(5).ToList();


            //ViewBag.ListFilmTopMark = ListFilmTopMark;
            ViewBag.ListFilmTopView = ListFilmTopView;
            ViewBag.ListFilmTopMark = ListFilmTopMark;


            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}