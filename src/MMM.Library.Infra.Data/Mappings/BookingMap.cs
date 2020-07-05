using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMM.Library.Domain.CQRS;

namespace MMM.Library.Infra.Data.Mappings
{
    public class BookingMap : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.BookingCode)
                .HasDefaultValueSql("NEXT VALUE FOR BookingCode");

            builder.ToTable("Bookings");
        }
    }
}
