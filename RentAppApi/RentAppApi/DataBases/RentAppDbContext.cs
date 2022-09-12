using System;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using RentAppApi.Tables;

namespace RentAppApi.DataBases
{
    public class RentAppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public RentAppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Califications>().ToTable("Califications");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<CategoryHistory>().ToTable("CategoryHistory");
            modelBuilder.Entity<Code>().ToTable("Code");
            modelBuilder.Entity<CodeHistory>().ToTable("CodeHistory");
            modelBuilder.Entity<Comments>().ToTable("Comments");
            modelBuilder.Entity<Favorites>().ToTable("Favorites");
            modelBuilder.Entity<ForgotPassword>().ToTable("ForgotPassword");
            modelBuilder.Entity<ImageProduct>().ToTable("ImageProduct");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<Notifications>().ToTable("Notifications");
            modelBuilder.Entity<Products>().ToTable("Products");
            modelBuilder.Entity<SubCategory>().ToTable("SubCategory");
            modelBuilder.Entity<SubCategoryHistory>().ToTable("SubCategoryHistory");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserHistory>().ToTable("UserHistory");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Califications> Califications { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryHistory> CategoryHistory { get; set; }
        public DbSet<Code> Code { get; set; }
        public DbSet<CodeHistory> CodeHistory { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<ForgotPassword> ForgotPassword { get; set; }
        public DbSet<ImageProduct> ImageProduct { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<SubCategoryHistory> SubCategoryHistory { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserHistory> UserHistory { get; set; }
    }
}

