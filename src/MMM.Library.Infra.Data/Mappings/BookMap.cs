using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMM.Library.Domain.Models;

namespace MMM.Library.Infra.Data.Mappings
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.ISBN)
              //.IsRequired()
              .HasColumnType("varchar(20)");

            builder.Property(p => p.Year)
                .IsRequired();

            builder.Property(p => p.Language)
                //.IsRequired()
                .HasColumnType("varchar(50)");

            // Audit Value Object
            //builder.OwnsOne(p => p.Audit, au =>
            //{
            //    au.Property(p => p.CreateUser).HasColumnName("CreateUser").HasColumnType("varchar(50)");
            //    au.Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("varchar(50)");
            //    au.Property(p => p.LastUpdateUser).HasColumnName("LastUpdateUser").HasColumnType("varchar(50)");
            //    au.Property(p => p.LastUpdateDate).HasColumnName("LastUpdateDate").HasColumnType("varchar(50)");

            //});

            // Query Filter for Soft Delete
            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.ToTable("Books");
        }
    }
}
