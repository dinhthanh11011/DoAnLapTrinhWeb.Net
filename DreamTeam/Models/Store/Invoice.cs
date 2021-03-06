using DreamTeam.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Store
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ngày Lập")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime CreateAt { get; set; }

        [Required]
        [Display(Name = "Thanh Toán")]
        public bool IsPayed { get; set; }

        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }

        public int InvoiceStatusId { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Người Nhận")]
        public string Person { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Số Điện Thoại")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Địa Điểm")]
        public string Location { get; set; }
    }
}