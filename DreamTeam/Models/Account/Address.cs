using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Account
{
    public class Address
    {
        public int Id { get; set; }

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

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}