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
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }
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
