using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ServiceCalendarConfiguration : IEntityTypeConfiguration<ServiceCalendar>
{
    public void Configure(EntityTypeBuilder<ServiceCalendar> builder)
    {
        builder.ToTable("ServiceCalendars").HasKey(sc => sc.Id);

        builder.Property(sc => sc.Id).HasColumnName("Id").IsRequired();
        builder.Property(sc => sc.Monday).HasColumnName("Monday").IsRequired();
        builder.Property(sc => sc.Tuesday).HasColumnName("Tuesday").IsRequired();
        builder.Property(sc => sc.Wednesday).HasColumnName("Wednesday").IsRequired();
        builder.Property(sc => sc.Thursday).HasColumnName("Thursday").IsRequired();
        builder.Property(sc => sc.Friday).HasColumnName("Friday").IsRequired();
        builder.Property(sc => sc.Saturday).HasColumnName("Saturday").IsRequired();
        builder.Property(sc => sc.Sunday).HasColumnName("Sunday").IsRequired();
        builder.Property(sc => sc.StartDate).HasColumnName("StartDate").IsRequired();
        builder.Property(sc => sc.EndDate).HasColumnName("EndDate").IsRequired();
        builder.Property(sc => sc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sc => sc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sc => sc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(sc => !sc.DeletedDate.HasValue);
    }
}