using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("Trips").HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
        builder.Property(t => t.RouteId).HasColumnName("RouteId").IsRequired();
        builder.Property(t => t.ServiceId).HasColumnName("ServiceId").IsRequired();
        builder.Property(t => t.TripHeadsign).HasColumnName("TripHeadsign").IsRequired();
        builder.Property(t => t.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(t => t.DeletedDate).HasColumnName("DeletedDate");

        // Navigation property relationships
        builder.HasOne(t => t.Route)
               .WithMany(r => r.Trips)
               .HasForeignKey(t => t.RouteId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.ServiceCalendar)
               .WithMany()
               .HasForeignKey(t => t.ServiceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.ShapePoints)
               .WithOne(s => s.Trip)
               .HasForeignKey(s => s.TripId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(t => !t.DeletedDate.HasValue);
    }
}