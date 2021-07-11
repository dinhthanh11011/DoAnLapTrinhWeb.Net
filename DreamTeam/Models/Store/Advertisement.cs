using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Store
{
    public class Advertisement
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Hình Ảnh")]
        public string Image { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Tiêu Đề")]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Mô Tả")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Trạng Thái")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Số Thứ Tự")]
        public int Ordering { get; set; }
    }
}