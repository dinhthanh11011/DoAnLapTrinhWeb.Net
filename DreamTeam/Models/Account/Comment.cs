using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Account
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Giá Trị")]
        public string Value { get; set; }

        [Required]
        public int Ratting { get; set; }

        public ApplicationUser Customer { get; set; }
        public Product.Product Product { get; set; }

        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        public ICollection<Comment_Img> Comment_Imgs { get; set; }
    }
}