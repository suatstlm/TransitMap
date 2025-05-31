using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StopConfiguration : IEntityTypeConfiguration<Stop>
{
    public void Configure(EntityTypeBuilder<Stop> builder)
    {
        builder.ToTable("Stops").HasKey(s => s.Id);

        builder.Property(s => s.Id).HasColumnName("Id").IsRequired();
        builder.Property(s => s.StopName).HasColumnName("StopName").IsRequired();
        builder.Property(s => s.StopLat).HasColumnName("StopLat").IsRequired();
        builder.Property(s => s.StopLon).HasColumnName("StopLon").IsRequired();
        builder.Property(s => s.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(s => s.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(s => s.DeletedDate).HasColumnName("DeletedDate");

        // Navigation property relationships
        builder.HasMany(s => s.StopTimes)
               .WithOne(st => st.Stop)
               .HasForeignKey(st => st.StopId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(s => !s.DeletedDate.HasValue);
    }
}