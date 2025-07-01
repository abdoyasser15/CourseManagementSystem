using Course.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Course.DAL.Data
{
    public class CourseManagementDbContext : IdentityDbContext<ApplicationUsers>
    {
        public CourseManagementDbContext(DbContextOptions<CourseManagementDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
