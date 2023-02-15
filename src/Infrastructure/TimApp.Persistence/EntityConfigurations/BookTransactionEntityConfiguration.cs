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
    public class BookTransactionEntityConfiguration : IEntityTypeConfiguration<BookTransaction>
    {
        public void Configure(EntityTypeBuilder<BookTransaction> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.RelatedMemberId).HasMaxLength(50);
            builder.Property(x => x.RelatedISBN).HasMaxLength(20);
            builder.Property(x => x.DelayPenaltyFee);

            builder.HasIndex(x => x.RelatedMemberId, "IX_RelatedMemberId");
            builder.HasIndex(x => x.RelatedISBN, "IX_RelatedISBN");
            builder.HasIndex(x => new { x.RelatedMemberId, x.RelatedISBN }, "IX_RelatedMemberId_RelatedISBN");

            builder.HasOne(m => m.Book)
                   .WithMany(t => t.BookTransactions)
                   .HasForeignKey(m => m.RelatedISBN)
                   .HasConstraintName("fk_books");

            builder.HasOne(m => m.Member)
                   .WithMany(t => t.BookTransactions)
                   .HasForeignKey(m => m.RelatedMemberId)
                   .HasConstraintName("fk_members");
        }
    }
}
