using System;
using System.ComponentModel.DataAnnotations;

namespace WallProject.Models
{
    public class Wall
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "عرض جغرافیایی")]
        public string Latitude { get; set; }
        [Display(Name = "طول جغرافیایی")]
        [Required]
        public string Longitude { get; set; }
       
        [Display(Name = "نام کاربر")]
        [Required]
        public ApplicationUser User { get; set; }
        [Display(Name = "آدرس")]
        [Required]
        public string Address { get; set; }

        public bool Confirm { get; set; }
    }
}