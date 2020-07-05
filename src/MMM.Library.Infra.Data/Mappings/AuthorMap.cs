using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMM.Library.Domain.Models;
using System;

namespace MMM.Library.Infra.Data.Mappings
{
    public class AuthorMap : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.BirthDate);

            builder.Property(p => p.Nationality)
                .IsRequired()
                .HasColumnType("varchar(50)");

            // Query Filter for Soft Delete
            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.ToTable("Authors");
        }        
    }
}
