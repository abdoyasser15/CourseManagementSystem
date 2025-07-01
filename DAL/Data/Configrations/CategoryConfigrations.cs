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
    public class CategoryConfigrations : IEntityTypeConfiguration<Categorie>
    {
        public void Configure(EntityTypeBuilder<Categorie> builder)
        {
            
        }
    }
}
