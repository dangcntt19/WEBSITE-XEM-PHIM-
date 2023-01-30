using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Areas.Admin.Controllers
{
    public class IMAGESController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();

        public ActionResult Images()
        {
            ViewBag.PagePositon = "IMAGES";
            var Images = _context.IMAGES.ToList();
            return View(Images);
        }

        [HttpPost]
        public JsonResult ReturnImages()
        {
            var Images = (from image in _context.IMAGES
                          join user in _context.USERS on image.USERID equals user.USERID
                          select new
                          {
                              IMAGEID = image.IMAGEID,
                              IMAGENAME = image.IMAGENAME,
                              DATECREATE = image.DATECREATE,
                              USERNAME = (user.FIRSTNAME + " " + user.LASTNAME).ToString(),
                          }).OrderByDescending(x=>x.IMAGEID).ToList();
            var json = JsonConvert.SerializeObject(Images);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteImage(int id)
        {
            var Image = _context.IMAGES.Where(x=>x.IMAGEID==id).FirstOrDefault();
            if(Image!=null)
            {
                _context.IMAGES.Remove(Image);
                _context.SaveChanges();
                return RedirectToAction("Images");
            }
            else
            {
                return RedirectToAction("Error", "MainDashboard");
            }
        }

    }
}