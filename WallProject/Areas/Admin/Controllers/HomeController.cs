using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WallProject.Controllers;

namespace WallProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController :ApplicationBaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}