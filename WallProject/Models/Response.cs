using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace WallProject.Models
{
    public class Response
    {
        
        public int Id { get; set; }
        [ForeignKey("Request")]
        public int  RequestId { get; set; }
        public Request Request { get; set; }
        [Display (Name = "پاسخ")]
        public bool Reply { get; set; }

    }
}