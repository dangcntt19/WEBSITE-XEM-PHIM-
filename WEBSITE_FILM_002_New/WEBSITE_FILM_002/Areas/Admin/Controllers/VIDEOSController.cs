using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Areas.Admin.Controllers
{
    public class VIDEOSController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();

        public ActionResult Videos()
        {
            ViewBag.PagePositon = "VIDEOS";
            return View();
        }

        public JsonResult Get_Videos()
        {
            var Videos = (from video in _context.VIDEOS
                          join user in _context.USERS on video.USERID equals user.USERID
                          select new
                          {
                              videoid = video.VIDEOID,
                              videoname = video.VIDEONAME,
                              datecreate = video.DATECREATE,
                              username = (user.LASTNAME + user.FIRSTNAME).ToString()
                          }).OrderByDescending(x=>x.videoid).ToList();
            var json = JsonConvert.SerializeObject(Videos);
            return Json(json, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult DeleteVideos(int id)
        {
            int Count = _context.VIDEOS.Where(x=>x.VIDEOID== id).Count();
            if(Count>0)
            {
                var Video = _context.VIDEOS.Where(x=>x.VIDEOID == id).FirstOrDefault();
                _context.VIDEOS.Remove(Video);
                _context.SaveChanges();
                return RedirectToAction("Videos");
            }
            return RedirectToAction("Error", "MainDashboard");
        }

    }
}