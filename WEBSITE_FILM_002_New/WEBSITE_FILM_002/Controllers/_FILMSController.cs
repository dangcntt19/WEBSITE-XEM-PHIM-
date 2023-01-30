using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Controllers
{
    public class _FILMSController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();


        //GET all films 
        [HttpPost]
        public JsonResult GET_FILMS()
        {
            var GET_Films = _context.FILMS.Where(x=>x.FILM_STATUS == 0).Select(x => new
            {
                filmid = x.FILMID,
                filmname = x.FILMNAME,
                status = x.STATUS,
                filmdirector = x.FILMDIRECTOR,
                productionyear = x.PRODUCTIONYEAR,
                time = x.TIME,
                quality = x.QUALITY,
                resolution = x.RESOLUTION,
                language = x.LANGUAGE,
                category = x.CATEGORY,
                views = x.VIEWS,
                manufacturing_conpany = x.MANUFACTURING_COMPANY,
                movie_review = x.MOVIEREVIEW,
                content_film = x.CONTENT_FILM,
                film_image = x.IMAGEID,
            }).OrderByDescending(x=>x.filmid).ToList();
            return Json(JsonConvert.SerializeObject(GET_Films), JsonRequestBehavior.AllowGet);
        }


        //GET Single FILMS
        [HttpGet]
        public JsonResult GET_FILM(int id)
        {
            var Film = (from x in _context.FILMS
                        where x.FILMID == (decimal)id && x.FILM_STATUS == 0
                        select new
                        {
                            film_id = id,
                            film_name = x.FILMNAME,
                            status = x.STATUS,
                            film_director = x.FILMDIRECTOR,
                            production_year = x.PRODUCTIONYEAR,
                            time = x.TIME,
                            quality = x.QUALITY,
                            resolution = x.RESOLUTION,
                            language = x.LANGUAGE,
                            category = x.CATEGORY,
                            views = x.VIEWS,
                            manufacturing_conpany = x.MANUFACTURING_COMPANY,
                            movie_review = x.MOVIEREVIEW,
                            content_film = x.CONTENT_FILM,
                            film_image = x.IMAGEID,
                        }).SingleOrDefault();
            var ConverJson = JsonConvert.SerializeObject(Film);
            return Json(ConverJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GET_Video_Film(int id)
        {
            var Video_Link = _context.FILMS.Where(x => x.FILMID.Equals(id) && x.FILM_STATUS == 0).Select(x => new { address = x.VIDEOID }).SingleOrDefault();
            if(Video_Link != null)
            {
                return Json(JsonConvert.SerializeObject(Video_Link), JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public JsonResult Get_TOP_Vieos()
        {
            var TopVideo = _context.FILMS.Select(x => new
            {
                filmid = x.FILMID,
                filmname = x.FILMNAME,
                status = x.STATUS,
                filmdirector = x.FILMDIRECTOR,
                productionyear = x.PRODUCTIONYEAR,
                time = x.TIME,
                quality = x.QUALITY,
                resolution = x.RESOLUTION,
                language = x.LANGUAGE,
                category = x.CATEGORY,
                views = x.VIEWS,
                manufacturing_conpany = x.MANUFACTURING_COMPANY,
                movie_review = x.MOVIEREVIEW,
                content_film = x.CONTENT_FILM,
                film_image = x.IMAGEID,
            }).Take(10).OrderByDescending(x => x.filmid);
            return Json(JsonConvert.SerializeObject(TopVideo), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewView(int id)
        {

            var _Film = _context.FILMS.Where(x => x.FILMID == (decimal)id).SingleOrDefault();

            if (_Film != null)
            {
                _Film.VIEWS = _Film.VIEWS + 1;
                _context.Entry(_Film).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return Json(new {data = "success"},JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Rating(decimal id, int rating)
        {

            if (rating >= 1 && rating <= 5)
            {
                var _Film = _context.FILMS.Where(x => x.FILMID == (decimal)id).SingleOrDefault();
                if (_Film != null)
                {
                    _Film.MOVIEREVIEW = _Film.MOVIEREVIEW == 0 ? rating : (_Film.MOVIEREVIEW + rating) / 2;
                    _context.Entry(_Film).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult get_film_()
        //{
        //    var TopVideo = (from f in _context.FILMS
        //                    where f.CATEGORY.ToString() == "phim"
        //                    select new
        //                    {
        //                        filmid = f.FILMID,
        //                        filmname = f.FILMNAME,
        //                    });
        //    return Json(JsonConvert.SerializeObject(TopVideo),JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult Get_Film_Take_10()
        {
            var TopVideo = (from f in _context.FILMS
                            where f.FILM_STATUS == 0
                            select new
                            {
                                filmid = f.FILMID,
                                filmname = f.FILMNAME,
                                images = f.IMAGEID
                            }).OrderByDescending(x=>x.filmid).Take(12);
            return Json(JsonConvert.SerializeObject(TopVideo), JsonRequestBehavior.AllowGet);
        }

        //chưa sử dụng
        public ActionResult GetFilm()
        {
            var _Flim = _context.FILMS.ToList();
            ViewBag.message = _Flim.Count();
            return View(_Flim);
        }

        //chưa sử dụng
        public ActionResult DetailFilm(decimal id)
        {
            var res = _context.FILMS.Where(x => x.FILMID == id).FirstOrDefault();

            return View(res);
        }
    }
}