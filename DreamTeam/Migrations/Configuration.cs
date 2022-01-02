namespace DreamTeam.Migrations
{
    using DreamTeam.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DreamTeam.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DreamTeam.Models.ApplicationDbContext context)
        {
            // insert role Acount Manage
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
            {
                Name = Support.support.ACCOUNT_MANAGE_PERMISSION
            });

            // insert role Product Manage
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
            {
                Name = Support.support.PRODUCT_MANAGE_PERMISSION
            });

            // insert role Store Manage
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole {
                Name = Support.support.STORE_MANAGE_PERMISSION
            });

            // insert base Invoice Status
            context.InvoiceStatuses.Add(new Models.Store.InvoiceStatus
            {
                Name = "Chưa xác nhận!",
                Ordering = 1,
                isDefault = true
            });

            context.SaveChanges();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var PasswordHash = new PasswordHasher();

            var userName = "dinhthanh11011@gmail.com";

            var user = new ApplicationUser { 
                UserName = userName, 
                Email = userName, 
                FullName = "dinhthanh11011", 
                Active = true,
                PasswordHash = PasswordHash.HashPassword("123456aA@")
            };

            UserManager.Create(user);

            UserManager.AddToRole(user.Id, Support.support.ACCOUNT_MANAGE_PERMISSION);
            UserManager.AddToRole(user.Id, Support.support.STORE_MANAGE_PERMISSION);
            UserManager.AddToRole(user.Id, Support.support.PRODUCT_MANAGE_PERMISSION);

            context.SaveChanges();

        }
    }
}
