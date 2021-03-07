using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Prj_Shop_Watch_Online.Models
{
    public partial class SWODBContext : DbContext
    {
        public SWODBContext()
            : base("name=SWODBContext")
        {
        }

        public virtual DbSet<AppFunction> AppFunction { get; set; }
        public virtual DbSet<AppFunctionGroupRole> AppFunctionGroupRole { get; set; }
        public virtual DbSet<AppGroupRole> AppGroupRole { get; set; }
        public virtual DbSet<AppList> AppList { get; set; }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Promotions> Promotions { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }

        public virtual DbSet<UserGroupRole> UserGroupRole { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brands>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Brands)
                .HasForeignKey(e => e.BrandId);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.OrderDetail)
                .WithRequired(e => e.Orders)
                .HasForeignKey(e => e.OrderId);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.OrderDetail)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.ProductImage)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.UserId);
        }
    }
}
