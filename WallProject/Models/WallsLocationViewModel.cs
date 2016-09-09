using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WallProject.Models
{
    public class WallsLocationViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام مکان")]
        public string PlaceName { get; set; }
        [Display(Name = "طول جغرافیایی")]
        public string GeoLong { get; set; }
        [Display(Name = "عرض جغرافیایی")]
        public string GeoLat { get; set; }
    }
}