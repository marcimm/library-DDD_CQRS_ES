using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMM.Library.Domain.Models;

namespace MMM.Library.Infra.Data.Mappings
{
    public class StockMap : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Edition)
                .IsRequired();

            builder.Property(p => p.Code)
                .HasDefaultValueSql("NEXT VALUE FOR StockCode");

            // Audit Value Object
            builder.OwnsOne(p => p.Audit, au =>
            {
                au.Property(p => p.CreateUser).HasColumnName("CreateUser").HasColumnType("varchar(50)");
                au.Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("varchar(50)");
                au.Property(p => p.LastUpdateUser).HasColumnName("LastUpdateUser").HasColumnType("varchar(50)");
                au.Property(p => p.LastUpdateDate).HasColumnName("LastUpdateDate").HasColumnType("varchar(50)");
            });

            // Query Filter for Soft Delete
            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.ToTable("Stocks");
        }
    }
}
