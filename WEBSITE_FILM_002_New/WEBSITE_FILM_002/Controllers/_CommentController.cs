using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBSITE_FILM_002.Models;

namespace WEBSITE_FILM_002.Controllers
{
    public class _CommentController : Controller
    {
        WEBSITE_FILM _context = new WEBSITE_FILM();

        [HttpGet]
        public JsonResult _GET_Comment_In(int id)
        {
            var _Com = (from cmt in _context.COMMENTS
                        join user in _context.USERS on cmt.USERID equals user.USERID
                        where cmt.COMMENT_STATUS == 0 && cmt.FILMID == id
                        select new
                        {
                            comment_content = cmt.COMMENT_CONTENT.ToString(),
                            date = cmt.DATECREATE.ToString(),
                            userFirstName = user.FIRSTNAME,
                            userLastName = user.LASTNAME,
                            avatar = user.IMAGENAME,
                        });
            return Json(JsonConvert.SerializeObject(_Com), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult NewComment(int filmid, int userid, string comment)
        {
            try
            {
                COMMENTS _cmt = new COMMENTS()
                {
                    COMMENT_CONTENT = comment,
                    DATECREATE = DateTime.Now,
                    USERID = userid,
                    FILMID = filmid,
                };
                _context.COMMENTS.Add(_cmt);
                _context.SaveChanges();
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}