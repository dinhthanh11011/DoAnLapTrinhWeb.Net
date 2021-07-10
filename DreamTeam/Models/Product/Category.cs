using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Product
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Tên Loại Sản Phẩm")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Ảnh Đại Diện")]
        public string Avatar { get; set; }

        [Required]
        [Display(Name = "Trạng Thái")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Số Thứ Tự")]
        public int Ordering { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<Attribute> Attributes { get; set; }
    }
}