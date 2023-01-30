using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Areas.Admin.Controllers
{
    public class FILMSController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();

        // GET: Admin/FILMS

        public ActionResult QLFILM()
        {
            ViewBag.PagePositon = "FILMS";
            var FILMS = _context.FILMS.ToList();
            return View(FILMS);
        }

        public ActionResult CreateFilm()
        {
            ViewBag.PagePositon = "FILMS";
            return View();
        }

        [HttpPost]
        public ActionResult CreateFilm(HttpPostedFileBase IMAGEID, HttpPostedFileBase VIDEOID, FILMS request)
        {
            try
            {
                if (IMAGEID.ContentLength > 0 &&
                VIDEOID.ContentLength > 0)
                {
                    string _Filname_Image = Path.GetFileName(IMAGEID.FileName);
                    string _path_Image = Path.Combine(Server.MapPath("~/Images/FilmPoster"), _Filname_Image);

                    string _FileName_Video = Path.GetFileName(VIDEOID.FileName);
                    string _path_video = Path.Combine(Server.MapPath("~/Videos/"), _FileName_Video);

                    //thêm ảnh vào cơ sở dữ liệu

                    IMAGES NewImage = new IMAGES()
                    {
                        IMAGENAME = _Filname_Image,
                        DATECREATE = DateTime.Now,
                        USERID = Convert.ToDecimal(Session["UserID"]),
                    };

                    VIDEOS NewVideo = new VIDEOS()
                    {
                        VIDEONAME = _FileName_Video,
                        DATECREATE = DateTime.Now,
                        USERID = Convert.ToDecimal(Session["UserID"]),
                    };

                    _context.IMAGES.Add(NewImage);
                    _context.VIDEOS.Add(NewVideo);

                    _context.SaveChanges();

                    //Thêm phim vào cơ sở dữ liệu
                    FILMS NewFilm = new FILMS()
                    {
                        FILMNAME = request.FILMNAME,
                        STATUS = request.STATUS,
                        FILMDIRECTOR = request.FILMDIRECTOR,
                        PRODUCTIONYEAR = request.PRODUCTIONYEAR,
                        TIME = request.TIME,
                        QUALITY = request.QUALITY,
                        RESOLUTION = request.RESOLUTION,
                        LANGUAGE = request.LANGUAGE,
                        CATEGORY = request.CATEGORY,
                        VIEWS = 0,
                        MANUFACTURING_COMPANY = request.MANUFACTURING_COMPANY,
                        MOVIEREVIEW = 0,
                        DATECREATE = DateTime.Now,
                        FILM_STATUS = 0,
                        CONTENT_FILM = request.CONTENT_FILM,
                        IMAGEID = _Filname_Image,
                        VIDEOID = _FileName_Video,
                        USERID = Convert.ToDecimal((int)Session["UserID"]),
                    };

                    _context.FILMS.Add(NewFilm);
                    _context.SaveChanges();

                    IMAGEID.SaveAs(_path_Image);
                    VIDEOID.SaveAs(_path_video);
                }
                return RedirectToAction("QLFILM");
            }
            catch (Exception)
            {
                return RedirectToAction("QLFILM");
            }
        }

        [HttpGet]
        public ActionResult EditFilm(int id)
        {
            if (id >= 0)
            {
                var Film = _context.FILMS.Where(x => x.FILMID == id).FirstOrDefault();
                if (Film != null)
                {
                    var _user = _context.USERS.Where(x=> x.USERID == Film.USERID).FirstOrDefault();
                    string firstname = _user.FIRSTNAME;
                    string lastname = _user.LASTNAME;
                    ViewBag.UserName = firstname + " " + lastname;

                    return View(Film);
                }
            }
            else
            {
                return null;
            }
            return null;
        }

        [HttpPost]
        public ActionResult EditFilm(HttpPostedFileBase IMAGEID, HttpPostedFileBase VIDEOID, FILMS request)
        {
            var Film = _context.FILMS.Where(x => x.FILMID == request.FILMID).FirstOrDefault();

            if (Film != null)
            {
                Film.FILMNAME = request.FILMNAME;
                Film.STATUS = request.STATUS;
                Film.FILMDIRECTOR = request.FILMDIRECTOR;
                Film.PRODUCTIONYEAR = request.PRODUCTIONYEAR;
                Film.TIME = request.TIME;
                Film.QUALITY = request.QUALITY;
                Film.RESOLUTION = request.RESOLUTION;
                Film.LANGUAGE = request.LANGUAGE;
                Film.CATEGORY = request.CATEGORY;
                Film.VIEWS = 0;
                Film.MANUFACTURING_COMPANY = request.MANUFACTURING_COMPANY;
                Film.MOVIEREVIEW = 0;
                Film.DATECREATE = DateTime.Now;
                Film.FILM_STATUS = 0;
                if (IMAGEID != null)
                {
                    string _Filname_Image = Path.GetFileName(IMAGEID.FileName);
                    string _path_Image = Path.Combine(Server.MapPath("~/Images/FilmPoster"), _Filname_Image);
                    if (!_Filname_Image.Equals(Film.IMAGEID))
                    {
                        Film.IMAGEID = _Filname_Image;
                        IMAGEID.SaveAs(_path_Image);
                        IMAGES NewImage = new IMAGES()
                        {
                            IMAGENAME = _Filname_Image,
                            DATECREATE = DateTime.Now,
                            USERID = Convert.ToDecimal(Session["UserID"]),
                        };
                        _context.IMAGES.Add(NewImage);
                    }
                }
                else
                {
                    ViewBag.Image = "Image is not null";
                }
                if (VIDEOID != null)
                {
                    string _FileName_Video = Path.GetFileName((string)VIDEOID.FileName);
                    string _path_video = Path.Combine(Server.MapPath("~/Videos/"), _FileName_Video);
                    if (!_FileName_Video.Equals((string)Film.VIDEOID))
                    {
                        Film.VIDEOID = _FileName_Video;
                        VIDEOID.SaveAs(_path_video);
                        VIDEOS NewVideo = new VIDEOS()
                        {
                            VIDEONAME = _FileName_Video,
                            DATECREATE = DateTime.Now,
                            USERID = Convert.ToDecimal(Session["UserID"]),
                        };
                        _context.VIDEOS.Add(NewVideo);
                    }
                }
                else
                {
                    ViewBag.Video = "video it not null";
                }
                Film.USERID = Convert.ToDecimal((int)Session["UserID"]);
                Film.CONTENT_FILM = request.CONTENT_FILM;
                _context.Entry(Film).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("/EditFilm/" + Film.FILMID, "FILMS");
            }
            return RedirectToAction("QLFILM");
        }

        [HttpGet]
        public ActionResult DeleteFilm(int id)
        {
            int Count = _context.FILMS.Where(x => x.FILMID == id).Count();
            if(Count == 1)
            {
                var Film = _context.FILMS.Where(x => x.FILMID == id).SingleOrDefault();
                _context.FILMS.Remove(Film);
                _context.SaveChanges();
                return RedirectToAction("QLFILM");
            }
            return View();
        }
    }
}