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
    internal class RatingConfigrations : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder
                .HasOne(R => R.Student)
                .WithMany(S => S.Raitings)
                .HasForeignKey(R => R.StudentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder
                .HasOne(R=>R.Course)
                .WithMany(C=>C.Ratings)
                .HasForeignKey(R=>R.CourseId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
