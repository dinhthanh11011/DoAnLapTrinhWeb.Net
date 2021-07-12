using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Account
{
    public class Cart
    {
        [Key]
        [Column(Order = 1)]
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ProductId { get; set; }
        public Product.Product Product { get; set; }
    }
}