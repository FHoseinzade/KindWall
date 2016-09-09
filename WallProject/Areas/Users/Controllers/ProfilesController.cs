using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WallProject.Areas.Users.Models;
using WallProject.Controllers;
using WallProject.Models;

namespace WallProject.Areas.Users.Controllers
{
    public class ProfilesController : ApplicationBaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users/Profiles/
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Requests");
            //return View();
        }

        public ActionResult ShowUserMenu()
        {
            var url = Request.Url.Segments[0] + Request.Url.Segments[1] + Request.Url.Segments[2] +
                      (Request.Url.Segments.Length >= 4 ? (Request.Url.Segments[3]) : "/Index");

            var currentUser = User.Identity.GetUserId();
            List<UserMenuViewModel> userMenuList = new List<UserMenuViewModel>();
            var requestMenu = new UserMenuViewModel()
            {
                MenuTitle = "درخواست های شما",
                Count = db.Requests.Count(r => r.OwnerUser.Id == currentUser),
                ActionName = "Index",
                ControllerName = "Requests"
            };
            var nazrMenu = new UserMenuViewModel()
            {
                MenuTitle = "نذر های شما",
                Count = db.Goods.Count(r => r.OwnerUser_Id == currentUser),
                ActionName = "Index",
                ControllerName = "Goods"
            };

            var responseMenu = new UserMenuViewModel()
            {
                MenuTitle = "درخواست های پاسخ داده نشده",
                //Count = db.Responses.Count(r=>r.),
                // تعداد درخواست هایی که به کالاهایی بوده است که مربوط به کاربر جاری است و پاسخی برای ان درخواست وجود ندارد
                Count = (from re in db.Requests
                    join g in db.Goods on re.Goods_Id equals g.Id
                    join res in db.Responses on re.Id equals res.Request.Id into ps
                    from res in ps.DefaultIfEmpty()
                    where g.OwnerUser_Id == currentUser && res == null
                    select re).Count(),
                ActionName = "UnAnswaredRequests",
                ControllerName = "Requests"
            };
            var wallsMenu = new UserMenuViewModel()
            {
                MenuTitle = "ثبت دیوار مهربانی",
                Count = db.Walls.Count(r => r.User.Id == currentUser && r.Confirm),
                ActionName = "Create",
                ControllerName = "Walls"
            };

            userMenuList.Add(requestMenu);
            userMenuList.Add(nazrMenu);
            userMenuList.Add(responseMenu);
            userMenuList.Add(wallsMenu);
            foreach (var userMenu in userMenuList)
            {
                userMenu.IsActive =
                    url.ToLower().Contains(userMenu.ControllerName.ToLower() + "/" + userMenu.ActionName.ToLower());
            }
            return PartialView("UserMenu", userMenuList);
        }
    }
}