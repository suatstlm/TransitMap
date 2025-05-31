using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable("Routes").HasKey(r => r.Id);

        builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
        builder.Property(r => r.AgencyId).HasColumnName("AgencyId").IsRequired();
        builder.Property(r => r.RouteShortName).HasColumnName("RouteShortName").IsRequired();
        builder.Property(r => r.RouteLongName).HasColumnName("RouteLongName").IsRequired();
        builder.Property(r => r.RouteType).HasColumnName("RouteType").IsRequired();
        builder.Property(r => r.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(r => r.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(r => r.DeletedDate).HasColumnName("DeletedDate");

        // Navigation property relationships
        builder.HasOne(r => r.Agency)
               .WithMany(a => a.Routes)
               .HasForeignKey(r => r.AgencyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(r => !r.DeletedDate.HasValue);
    }
}