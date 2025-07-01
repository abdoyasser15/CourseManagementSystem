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
    public class ApplicationUserConfigrations : IEntityTypeConfiguration<ApplicationUsers>
    {
        public void Configure(EntityTypeBuilder<ApplicationUsers> builder)
        {
            //Role
            builder.Property(u => u.Role)
                   .HasMaxLength(20)
                   .IsRequired();

            builder
                .HasMany(U => U.Course)
                .WithOne(C => C.Instructor)
                .HasForeignKey(C=>C.InstructorId)
                .IsRequired();
        }
    }
}
