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
    public class CourseConfigrations : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> builder)
        {
            builder
                .HasOne(C => C.Categorie)
                .WithMany(Ca => Ca.Courses)
                .HasForeignKey(C => C.CategoryID)
                .IsRequired();

            builder.Property(c => c.Price)
                    .HasColumnType("decimal(10,2)");
        }
    }
}
