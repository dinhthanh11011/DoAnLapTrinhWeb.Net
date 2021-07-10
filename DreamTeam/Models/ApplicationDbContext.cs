using DreamTeam.Models.Account;
using DreamTeam.Models.Product;
using DreamTeam.Models.Store;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DreamTeam.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Comment_Img> Comment_Imgs { get; set; }
        public DbSet<DreamTeam.Models.Product.Attribute> Attributes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Brand_Img> Brand_Imgs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product.Product> Products { get; set; }
        public DbSet<Product_Attribute> Product_Attributes { get; set; }
        public DbSet<Product.Product_Img> Product_Imgs { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        public DbSet<Logo> Logos { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product_Attribute>()
                .HasRequired(x => x.Attribute)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasRequired(x => x.InvoiceStatus)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasRequired(x => x.Brand)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product.Product>()
                .HasRequired(x => x.Category)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}