using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WallProject.Controllers;
using WallProject.Models;

namespace WallProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : ApplicationBaseController
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult NewsList()
        {
            return View();
        }

        public ActionResult AddNews()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNews(News news)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.News.Add(news);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    //log zadane khata va namayesh error monaseb be karbar
                    Debug.WriteLine(e.Message);
                }
            }
            return View(news);
        }

        public ActionResult EditNews(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var news = _context.News.SingleOrDefault(n=>n.Id == id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost]
        public ActionResult EditNews(News news)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(news).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return View(news);
        }
    }
}