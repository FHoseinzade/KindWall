using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using Newtonsoft.Json;

namespace WallProject.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        //public List<RequestDetail> RequestDetails { get; set; }
        [Display(Name = "تعداد")]
        public int Count { get; set; }
        

        [Display(Name = "نام کالا")]
        [ForeignKey("Goods_Id")]
        public Goods Goods { get; set; }
        
        public int? Goods_Id { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime DateTime { get; set; }
        
        [Display(Name = "نام کاربر")]
        [ForeignKey("OwnerUser_Id")]
        public ApplicationUser OwnerUser { get; set; }
        public string OwnerUser_Id { get; set; }

    }
}