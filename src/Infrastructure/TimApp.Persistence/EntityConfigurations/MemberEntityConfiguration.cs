using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimApp.Domain.Entities;

namespace TimApp.Persistence.EntityConfigurations
{
    public class MemberEntityConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.MemberId).HasMaxLength(50);
            builder.HasKey(x => x.MemberId);
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Surname).HasMaxLength(100);

        }
    }
}
