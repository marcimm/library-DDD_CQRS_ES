using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMM.Library.Domain.CQRS;

namespace MMM.Library.Infra.Data.Mappings
{
    public class BookingItemMap : IEntityTypeConfiguration<BookingItem>
    {
        public void Configure(EntityTypeBuilder<BookingItem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DateStart)
                .IsRequired();

            builder.Property(p => p.DateEnd)
                .IsRequired();

            builder.Property(p => p.Notes)
                .HasColumnType("varchar(500)");

            builder.Property(p => p.BookingItemCode)
               .HasDefaultValueSql("NEXT VALUE FOR BookingItemCode");

            builder.ToTable("BookingItems");
        }
    }
}
