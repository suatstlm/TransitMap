using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ShapeConfiguration : IEntityTypeConfiguration<Shape>
{
    public void Configure(EntityTypeBuilder<Shape> builder)
    {
        builder.ToTable("Shapes").HasKey(s => s.Id);

        builder.Property(s => s.Id).HasColumnName("Id").IsRequired();
        builder.Property(s => s.TripId).HasColumnName("TripId").IsRequired();
        builder.Property(s => s.ShapeLat).HasColumnName("ShapeLat").IsRequired();
        builder.Property(s => s.ShapeLon).HasColumnName("ShapeLon").IsRequired();
        builder.Property(s => s.ShapePtSequence).HasColumnName("ShapePtSequence").IsRequired();
        builder.Property(s => s.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(s => s.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(s => s.DeletedDate).HasColumnName("DeletedDate");

        // Navigation property relationships
        builder.HasOne(s => s.Trip)
               .WithMany(t => t.ShapePoints)
               .HasForeignKey(s => s.TripId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(s => !s.DeletedDate.HasValue);
    }
}