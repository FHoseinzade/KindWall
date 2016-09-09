using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WallProject.Areas.Users.Models
{
    public class UserMenuViewModel
    {
        public string MenuTitle { get; set; }
        public int Count { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public bool IsActive { get; set; }

    }
}