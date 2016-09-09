using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Facebook;
using WallProject.Controllers;
using WallProject.Models;

namespace WallProject.Areas.Users.Controllers
{
    public class RequestsController : ApplicationBaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users/Requests
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UnAnswaredRequests()
        {
            var currentUser = User.Identity.GetUserId();

            List<Request> requests = (from re in db.Requests
                                      join g in db.Goods on re.Goods_Id equals g.Id
                                      join res in db.Responses on re.Id equals res.Request.Id into ps
                                      from res in ps.DefaultIfEmpty()
                                      where g.OwnerUser_Id == currentUser && res == null
                                      select re).Include(r=>r.Goods).Include(o=>o.OwnerUser).ToList();
            

            return View(requests);
        }


        public ActionResult ShowList()
        {
            var currentUser = User.Identity.GetUserId();
            return PartialView(db.Requests.Where(r=>r.OwnerUser.Id == currentUser).Include(g => g.Goods).ToList());
        }

        // GET: Users/Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Users/Requests/Create?goods=value
        public ActionResult Create(int goodsId)
        {
            Goods findedGoods = db.Goods.Find(goodsId);
            if (findedGoods == null)
            {
                return HttpNotFound();
            }
            var request = new Request();
            request.Goods_Id = goodsId;
            ViewBag.GoodsTitle = findedGoods.Title;
            
            return View(request);
        }

        // POST: Users/Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Request request)
        {
            if (ModelState.IsValid)
            {
                if (request.Count == 0)
                {
                    ViewBag.GoodsTitle = db.Goods.Single(g => g.Id == request.Goods_Id).Title;
                    ModelState.AddModelError("Count","تعداد کالای درخواستی نمیتواند 0 باشد.");
                    return View(request);
                }
                //request.Goods = db.Goods.Find(Convert.ToInt32(GoodsId));
                ////ToDo: در این قسمت باید چک شود که آیا در این لحظه کالا موجود می باشد یا خیر؟
                
                request.DateTime=DateTime.Now;
                request.OwnerUser_Id = User.Identity.GetUserId();
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Users/Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Include(g=>g.Goods).SingleOrDefault(r=>r.Id==id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.GoodsTitle = request.Goods.Title;
            return PartialView(request);
        }

        // POST: Users/Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Count,DateTime,Goods_Id")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("ShowList",db.Requests.Include(r=>r.Goods).ToList());
            }
            return View(request);
        }

        // GET: Users/Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Include(g=>g.Goods).SingleOrDefault(r=>r.Id==id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.GoodsTitle = request.Goods.Title;
            return PartialView(request);
        }

        // POST: Users/Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
            db.SaveChanges();
            return PartialView("ShowList",db.Requests.Include(r=>r.Goods).ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
