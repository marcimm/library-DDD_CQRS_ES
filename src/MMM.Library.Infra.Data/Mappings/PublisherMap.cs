using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMM.Library.Domain.Models;
using System;

namespace MMM.Library.Infra.Data.Mappings
{
    public class PublisherMap : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Phone)
                .IsRequired()
                .HasColumnType("varchar(30)");

            // Query Filter for Soft Delete
            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.ToTable("Publishers");
        }
    }
}
