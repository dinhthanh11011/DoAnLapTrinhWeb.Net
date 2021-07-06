using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Store
{
    public class InvoiceDetail
    {
        public Invoice Invoice { get; set; }

        public Product.Product Product { get; set; }

        [Key, Column(Order = 1)]
        public int InvoiceId { get; set; }
        [Key, Column(Order = 2)]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Số Lượng")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Đơn Giá")]
        public long Price { get; set; }
    }
}