using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Microsoft.Owin.Security.OAuth;

namespace WallProject.Models
{
    public class Goods
    {
        // error on here
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "نام خود را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "عکس")]
        public string PictureName { get; set; }

        [Display(Name = "تاریخ")]
        [Required(ErrorMessage = "(*)")]
        // Location: Views\Shared\EditorTemplates\PersianDatePicker.cshtml
        [UIHint("PersianDatePicker")]
        public DateTime DateTime { get; set; }

        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "تعداد را مشخص کنید")]
        public int Count { get; set; }

        [Display(Name = "امکان ارسال")]
        public bool CanSend { get; set; }
        [ForeignKey("OwnerUser")]
        public string OwnerUser_Id { get; set; }

        [Display(Name = "نام کاربر")]
        public ApplicationUser OwnerUser { get; set; }

        [Display(Name = "تعداد موجود")]
        public int Available { get; set; }
    }
}