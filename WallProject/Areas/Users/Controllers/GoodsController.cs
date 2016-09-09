using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Google;
using WallProject.Controllers;
using WallProject.Models;

namespace WallProject.Areas.Users.Controllers
{
    public class GoodsController : ApplicationBaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users/Goods
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            var goods = db.Goods.Where(g => g.OwnerUser_Id == currentUser).ToList();
            return View(goods);
        }
   
        public ActionResult Nazr()
        {
            var currentUser = User.Identity.GetUserId();
            var goodsList = db.Goods.Where(g=>g.OwnerUser_Id != currentUser).ToList();
            return View(goodsList);
        }


        // GET: Users/Goods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        // GET: Users/Goods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Goods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,DateTime,Count,CanSend")] Goods goods, HttpPostedFileBase myPic)
        {
            if (ModelState.IsValid)
            {
                var currentUser = db.Users.Find(User.Identity.GetUserId());
                goods.OwnerUser = currentUser;
                goods.PictureName = "no-photo.jpg";
                if (myPic != null)
                {
                    //MemoryStream stream = new MemoryStream();
                    //myPic.InputStream.CopyTo(stream);
                    goods.PictureName = Guid.NewGuid().ToString()+".jpg";
                    myPic.SaveAs(Server.MapPath("/IMG/"+goods.PictureName));
                }
                goods.Available = goods.Count;
                db.Goods.Add(goods);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goods);
        }

        // GET: Users/Goods/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goods goods = db.Goods.SingleOrDefault(o => o.Id == id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        // POST: Users/Goods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,PictureName,DateTime,Count,CanSend,OwnerUser_Id")] Goods goods, HttpPostedFileBase myPic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goods).State = EntityState.Modified;
                if (myPic != null) //اگه عکس انتخاب کرده بود جایگذین قبلی کن اگه نکرده بود همون قبلی باشه
                {
                    //MemoryStream stream = new MemoryStream();
                    //myPic.InputStream.CopyTo(stream);
                    if (goods.PictureName != "no-photo.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("/IMG/"+goods.PictureName));
                    }
                    goods.PictureName = Guid.NewGuid().ToString()+".jpg";
                    myPic.SaveAs(Server.MapPath("/IMG/"+goods.PictureName));
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goods);
        }

        // GET: Users/Goods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        // POST: Users/Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Goods goods = db.Goods.Find(id);
            db.Goods.Remove(goods);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // To convert the Byte Array to the author Image
        //public FileContentResult GetImage(int id)
        //{
        //    byte[] byteArray = db.Goods.Find(id).Picture;
        //    return byteArray != null
        //        ? new FileContentResult(byteArray, "image/jpeg")
        //        : null;
        //}

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
