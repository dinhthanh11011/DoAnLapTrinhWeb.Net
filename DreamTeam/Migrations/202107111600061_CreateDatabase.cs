namespace DreamTeam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Person = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(nullable: false, maxLength: 10),
                        Location = c.String(nullable: false, maxLength: 255),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(nullable: false, maxLength: 255),
                        Active = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Quantity = c.Int(nullable: false),
                        OldPrice = c.Long(nullable: false),
                        CurrentPrice = c.Long(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Hot = c.Boolean(nullable: false),
                        New = c.Boolean(nullable: false),
                        Ordering = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Avatar = c.String(nullable: false, maxLength: 255),
                        Active = c.Boolean(nullable: false),
                        Ordering = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Attributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Code = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Ordering = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Logo = c.String(nullable: false, maxLength: 255),
                        Active = c.Boolean(nullable: false),
                        Ordering = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brand_Img",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Active = c.Boolean(nullable: false),
                        Ordering = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 255),
                        Ratting = c.Int(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Comment_Img",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        CommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.Product_Attribute",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        AttributeId = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => new { t.ProductId, t.AttributeId })
                .ForeignKey("dbo.Attributes", t => t.AttributeId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.AttributeId);
            
            CreateTable(
                "dbo.Product_Img",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Active = c.Boolean(nullable: false),
                        Ordering = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(nullable: false, maxLength: 255),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false, maxLength: 255),
                        Active = c.Boolean(nullable: false),
                        Ordering = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.InvoiceId, t.ProductId })
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateAt = c.DateTime(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                        InvoiceStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .ForeignKey("dbo.InvoiceStatus", t => t.InvoiceStatusId)
                .Index(t => t.CustomerId)
                .Index(t => t.InvoiceStatusId);
            
            CreateTable(
                "dbo.InvoiceStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        isDefault = c.Boolean(nullable: false),
                        Ordering = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.InvoiceDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Invoices", "InvoiceStatusId", "dbo.InvoiceStatus");
            DropForeignKey("dbo.InvoiceDetails", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Product_Img", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Product_Attribute", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Product_Attribute", "AttributeId", "dbo.Attributes");
            DropForeignKey("dbo.Comments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Comments", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comment_Img", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Brand_Img", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Attributes", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Invoices", new[] { "InvoiceStatusId" });
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            DropIndex("dbo.InvoiceDetails", new[] { "ProductId" });
            DropIndex("dbo.InvoiceDetails", new[] { "InvoiceId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Product_Img", new[] { "ProductId" });
            DropIndex("dbo.Product_Attribute", new[] { "AttributeId" });
            DropIndex("dbo.Product_Attribute", new[] { "ProductId" });
            DropIndex("dbo.Comment_Img", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "ProductId" });
            DropIndex("dbo.Comments", new[] { "CustomerId" });
            DropIndex("dbo.Brand_Img", new[] { "BrandId" });
            DropIndex("dbo.Attributes", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Logoes");
            DropTable("dbo.InvoiceStatus");
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceDetails");
            DropTable("dbo.Advertisements");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Product_Img");
            DropTable("dbo.Product_Attribute");
            DropTable("dbo.Comment_Img");
            DropTable("dbo.Comments");
            DropTable("dbo.Brand_Img");
            DropTable("dbo.Brands");
            DropTable("dbo.Attributes");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Addresses");
        }
    }
}
