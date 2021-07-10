namespace DreamTeam.Migrations
{
    using DreamTeam.Models;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertBaseData : DbMigration
    {
        public override void Up()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            dbContext.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
            {
                Name = Support.support.ACCOUNT_MANAGE_PERMISSION
            });
            dbContext.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
            {
                Name = Support.support.PRODUCT_MANAGE_PERMISSION
            });
            dbContext.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = Support.support.STORE_MANAGE_PERMISSION });
            dbContext.SaveChanges();
        }
        
        public override void Down()
        {
        }
    }
}
