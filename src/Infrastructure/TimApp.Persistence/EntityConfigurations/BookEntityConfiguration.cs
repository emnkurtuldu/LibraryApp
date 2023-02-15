using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TimApp.Domain.Entities;

namespace TimApp.Persistence.EntityConfigurations
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.ISBN).HasMaxLength(20);
            builder.HasKey(x => x.ISBN);
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.Property(x => x.Author).HasMaxLength(200);

            builder.HasIndex(x => x.Name, "IX_Name");
            builder.HasIndex(x => x.Author, "IX_Author");
            builder.HasIndex(x => new { x.Name, x.Author }, "IX_Name_Author");
        }
    }
}
