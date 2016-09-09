using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using WallProject.Controllers;
using WallProject.Models;

namespace WallProject.Areas.Users.Controllers
{
    public class WallsController : ApplicationBaseController
    {
        ApplicationDbContext db  = new ApplicationDbContext();
        // GET: Users/Walls
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string jsonStr)
        {
            string currentUser = User.Identity.GetUserId();
            List<Wall> wallLocations = new List<Wall>();
            JArray jsonobj = JArray.Parse(jsonStr);
            //var myArray = jsonobj.Children<JProperty>().FirstOrDefault().Value;
            foreach (JToken item in jsonobj.Children())
            {
                JEnumerable<JProperty> itemProperties = item.Children<JProperty>();
                JProperty lat = itemProperties.FirstOrDefault(x => x.Name == "lat");
                string latValue = lat.Value.ToString();
                var lng = itemProperties.FirstOrDefault(x => x.Name == "lng");
                string lngValue = lng.Value.ToString();
                var wall = new Wall();
                wall.Latitude = latValue.Replace("/",".");
                wall.Longitude = lngValue.Replace("/",".");
                wall.User = db.Users.Find(currentUser);
                wall.Address = "تعریف نشده";
                wallLocations.Add(wall);
            }
            try
            {
                db.Walls.AddRange(wallLocations);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index", "Profiles");
        }
    }
}