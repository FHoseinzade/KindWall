using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using WallProject.Models;


namespace WallProject.Controllers
{
    public class HomeController : ApplicationBaseController
    {
        ApplicationDbContext context=new ApplicationDbContext();

        // Just for training
        //public ActionResult FindCountry(string countryName)
        //{
        //    ViewBag.Parameter = countryName;
        //    return View();
        //}

        public ActionResult Index()
        {
            List<Goods> goods = context.Goods.OrderByDescending(g=>g.DateTime).Take(4).ToList();
            ViewBag.LastestNazrs = goods;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Contactus()
        {
            return View();
        }

        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult NazreNo()
        {
            return View();
        }

        public JsonResult GetWalls()
        {
            var wallsList = context.Walls.Where(w=>w.Confirm).ToList();
            List<WallsLocationViewModel> wallsLocationViewModels = new List<WallsLocationViewModel>();
            foreach (var wall in wallsList)
            {
                wallsLocationViewModels.Add(new WallsLocationViewModel()
                {
                    Id = wall.Id,
                    GeoLat = wall.Latitude,
                    GeoLong = wall.Longitude,
                    PlaceName = wall.Address
                });
            }
            return Json(wallsLocationViewModels,JsonRequestBehavior.AllowGet);
        }
    }
}