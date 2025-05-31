using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AgencyConfiguration : IEntityTypeConfiguration<Agency>
{
    public void Configure(EntityTypeBuilder<Agency> builder)
    {
        builder.ToTable("Agencies").HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
        builder.Property(a => a.AgencyName).HasColumnName("AgencyName").IsRequired();
        builder.Property(a => a.AgencyUrl).HasColumnName("AgencyUrl").IsRequired();
        builder.Property(a => a.AgencyTimezone).HasColumnName("AgencyTimezone").IsRequired();
        builder.Property(a => a.AgencyLang).HasColumnName("AgencyLang").IsRequired();
        builder.Property(a => a.AgencyPhone).HasColumnName("AgencyPhone").IsRequired();
        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

        // Navigation property relationships
        builder.HasMany(a => a.Routes)
               .WithOne(r => r.Agency)
               .HasForeignKey(r => r.AgencyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}