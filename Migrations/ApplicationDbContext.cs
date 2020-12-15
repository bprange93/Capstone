using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlannerProject.Models;

namespace PlannerProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<Child> Child { get; set; }
        public DbSet<ChoreList> ChoreList { get; set; }
        public DbSet<ParentChildJunction> ParentChildJunction { get; set; }
        public DbSet<ChoreItem> ChoreItem { get; set; }
        public DbSet<ParentsTask> ParentsTask { get; set; }
        public DbSet<ParentItem> ParentItem { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Name = "Parent",
                        NormalizedName = "PARENT"
                    },
                     new IdentityRole
                     {
                         Name = "Child",
                         NormalizedName = "CHILD"
                     }

            );
           
        }
       
    }
}
