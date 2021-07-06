using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Store
{
    public class InvoiceStatus
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Trạng Thái Đơn Hàng")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Mặc Định")]
        public bool isDefault { get; set; }

        [Required]
        [Display(Name = "Số Thứ Tự")]
        public int Ordering { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}