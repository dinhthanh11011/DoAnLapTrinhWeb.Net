using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using DreamTeam.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DreamTeam.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Address> Addresses{ get; set; }
        public ICollection<Product.Product> Carts{ get; set; }
        public ICollection<Comment> Comments{ get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name ="Họ Tên")]
        public string FullName { get; set; }


        [Required]
        [Display(Name = "Trạng Thái")]
        public bool Active { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    
}