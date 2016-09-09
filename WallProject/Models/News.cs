using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WallProject.Models
{
    public class News
    {
        public int Id { get; set; }
        [Display(Name = "عنوان خبر")]
        public string Title { get; set; }
        [Display(Name = "متن خبر")]
        public string Body { get; set; }
        [Display(Name = "تاریخ خبر")]
        public DateTime NewsDate { get; set; }
        [Display(Name = "گروه خبر")]
        public NewsCategory NewsCategory { get; set; }
    }
}