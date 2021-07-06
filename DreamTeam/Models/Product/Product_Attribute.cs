using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Product
{
    public class Product_Attribute
    {
        public Product Product { get; set; }

        public Attribute Attribute { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int AttributeId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Giá Trị")]
        public string Value { get; set; }
    }
}