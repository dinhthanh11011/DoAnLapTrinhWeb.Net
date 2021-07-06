using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DreamTeam.Models.Account
{
    public class Comment_Img
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Tên Hình")]
        public string Name { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}