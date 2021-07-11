using DreamTeam.Models.Account;
using DreamTeam.Models.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Product
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Tên Sản Phẩm")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Số Lượng")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Giá Cũ")]
        public long OldPrice { get; set; }

        [Required]
        [Display(Name = "Giá Hiện Tại")]
        public long CurrentPrice { get; set; }

        [Required]
        [Display(Name = "Trạng Thái")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Nỗi bật")]
        public bool Hot { get; set; }

        [Required]
        [Display(Name = "Mới về")]
        public bool New { get; set; }

        [Required]
        [Display(Name = "Số Thứ Tự")]
        public int Ordering { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Product_Img> Product_Imgs { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Product_Attribute> Product_Attributes { get; set; }
    }
}