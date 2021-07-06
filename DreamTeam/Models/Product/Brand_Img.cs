using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Product
{
    public class Brand_Img
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Tên Hình")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Trạng Thái")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Số Thứ Tự")]
        public int Ordering { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}