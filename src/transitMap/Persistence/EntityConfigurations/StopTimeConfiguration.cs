using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StopTimeConfiguration : IEntityTypeConfiguration<StopTime>
{
    public void Configure(EntityTypeBuilder<StopTime> builder)
    {
        builder.ToTable("StopTimes").HasKey(st => st.Id);

        builder.Property(st => st.Id).HasColumnName("Id").IsRequired();
        builder.Property(st => st.TripId).HasColumnName("TripId").IsRequired();
        builder.Property(st => st.StopId).HasColumnName("StopId").IsRequired();
        builder.Property(st => st.ArrivalTime).HasColumnName("ArrivalTime").IsRequired();
        builder.Property(st => st.DepartureTime).HasColumnName("DepartureTime").IsRequired();
        builder.Property(st => st.StopSequence).HasColumnName("StopSequence").IsRequired();
        builder.Property(st => st.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(st => st.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(st => st.DeletedDate).HasColumnName("DeletedDate");

        // Navigation property relationships
        builder.HasOne(st => st.Trip)
               .WithMany(t => t.StopTimes)
               .HasForeignKey(st => st.TripId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(st => st.Stop)
               .WithMany(s => s.StopTimes)
               .HasForeignKey(st => st.StopId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(st => !st.DeletedDate.HasValue);
    }
}