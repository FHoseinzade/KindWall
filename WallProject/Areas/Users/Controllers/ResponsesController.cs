using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WallProject.Controllers;
using WallProject.Models;

namespace WallProject.Areas.Users.Controllers
{
    public class ResponsesController : ApplicationBaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users/Responses
        public ActionResult Index()
        {
            return View(db.Responses.ToList());
        }
        

        // GET: Users/Responses/Create/4
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Request Id</param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            if (db.Responses.Any(r => r.RequestId == id)) // اگر به این درخواست قبلا پاسخی داده شده بود
            {
                return View("ResponseError");
            }
            Request request = db.Requests.Include(r=>r.Goods).Include(r=>r.OwnerUser).Single(r => r.Id == id);
            var response = new Response();
            response.RequestId = id;
            ViewBag.GoodsTitle = request.Goods.Title;
            ViewBag.UserName = request.OwnerUser.Name.ToString() + request.OwnerUser.Family.ToString();
            ViewBag.DateTime = request.DateTime;
            ViewBag.Count = request.Count;
            return View(response);
        }

        // POST: Users/Responses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequestId,Reply")] Response response,string answare)
        {
            if (ModelState.IsValid)
            {
                if (answare == "بله")
                {
                    response.Reply = true;
                    var request = db.Requests.Include(r=>r.OwnerUser).Single(r=>r.Id == response.RequestId);
                    var goods = db.Goods.Find(request.Goods_Id);
                    if (goods.Available - request.Count < 0)
                    {
                        ModelState.AddModelError("Reply","در حال حاضر تعداد درخواستی از تعداد موجودی بیش تر است.");

                        ViewBag.GoodsTitle = request.Goods.Title;
                        ViewBag.UserName = request.OwnerUser.Name.ToString() +" " +request.OwnerUser.Family.ToString();
                        ViewBag.DateTime = request.DateTime;
                        ViewBag.Count = request.Count;

                        return View(response);
                    }

                    goods.Available = goods.Available - request.Count;
                }

                db.Responses.Add(response);
                db.SaveChanges();
                return RedirectToAction("UnAnswaredRequests","Requests");
            }

            return View(response);
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
