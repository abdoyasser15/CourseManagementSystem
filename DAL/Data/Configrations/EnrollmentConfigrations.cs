using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Data.Configrations
{
    public class EnrollmentConfigrations : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder
                .HasOne(E => E.Course)
                .WithMany(C => C.Enrollments)
                .HasForeignKey(E=>E.CourseId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder
                .HasOne(E => E.Student)
                .WithMany(U => U.Enrollments)
                .HasForeignKey(E => E.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
