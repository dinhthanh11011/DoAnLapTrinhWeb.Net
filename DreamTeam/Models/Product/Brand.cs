using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Product
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Tên Thương Hiệu")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Logo")]
        public string Logo { get; set; }

        [Required]
        [Display(Name = "Trạng Thái")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Số Thứ Tự")]
        public int Ordering { get; set; }

        public ICollection<Brand_Img> Brand_Imgs { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}