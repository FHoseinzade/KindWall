using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WallProject.Models
{
    public class SearchViewModel
    {
        [Display(Name = "محله")]
        public string SearchLocation { get; set; }
    }
}