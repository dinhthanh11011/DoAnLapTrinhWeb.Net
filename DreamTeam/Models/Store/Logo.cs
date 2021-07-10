using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Store
{
    public class Logo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Logo")]
        public string Name { get; set; }
    }
}