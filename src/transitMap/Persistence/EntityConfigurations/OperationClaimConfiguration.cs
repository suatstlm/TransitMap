using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Security.Constants;
using Application.Features.Agencies.Constants;
using Application.Features.Routes.Constants;
using Application.Features.ServiceCalendars.Constants;
using Application.Features.Shapes.Constants;
using Application.Features.Stops.Constants;
using Application.Features.StopTimes.Constants;
using Application.Features.Trips.Constants;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        
        #region Agencies CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AgenciesOperationClaims.Admin },
                new() { Id = ++lastId, Name = AgenciesOperationClaims.Read },
                new() { Id = ++lastId, Name = AgenciesOperationClaims.Write },
                new() { Id = ++lastId, Name = AgenciesOperationClaims.Create },
                new() { Id = ++lastId, Name = AgenciesOperationClaims.Update },
                new() { Id = ++lastId, Name = AgenciesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Routes CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = RoutesOperationClaims.Admin },
                new() { Id = ++lastId, Name = RoutesOperationClaims.Read },
                new() { Id = ++lastId, Name = RoutesOperationClaims.Write },
                new() { Id = ++lastId, Name = RoutesOperationClaims.Create },
                new() { Id = ++lastId, Name = RoutesOperationClaims.Update },
                new() { Id = ++lastId, Name = RoutesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region ServiceCalendars CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ServiceCalendarsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ServiceCalendarsOperationClaims.Read },
                new() { Id = ++lastId, Name = ServiceCalendarsOperationClaims.Write },
                new() { Id = ++lastId, Name = ServiceCalendarsOperationClaims.Create },
                new() { Id = ++lastId, Name = ServiceCalendarsOperationClaims.Update },
                new() { Id = ++lastId, Name = ServiceCalendarsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Shapes CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ShapesOperationClaims.Admin },
                new() { Id = ++lastId, Name = ShapesOperationClaims.Read },
                new() { Id = ++lastId, Name = ShapesOperationClaims.Write },
                new() { Id = ++lastId, Name = ShapesOperationClaims.Create },
                new() { Id = ++lastId, Name = ShapesOperationClaims.Update },
                new() { Id = ++lastId, Name = ShapesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Stops CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = StopsOperationClaims.Admin },
                new() { Id = ++lastId, Name = StopsOperationClaims.Read },
                new() { Id = ++lastId, Name = StopsOperationClaims.Write },
                new() { Id = ++lastId, Name = StopsOperationClaims.Create },
                new() { Id = ++lastId, Name = StopsOperationClaims.Update },
                new() { Id = ++lastId, Name = StopsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region StopTimes CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = StopTimesOperationClaims.Admin },
                new() { Id = ++lastId, Name = StopTimesOperationClaims.Read },
                new() { Id = ++lastId, Name = StopTimesOperationClaims.Write },
                new() { Id = ++lastId, Name = StopTimesOperationClaims.Create },
                new() { Id = ++lastId, Name = StopTimesOperationClaims.Update },
                new() { Id = ++lastId, Name = StopTimesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Trips CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = TripsOperationClaims.Admin },
                new() { Id = ++lastId, Name = TripsOperationClaims.Read },
                new() { Id = ++lastId, Name = TripsOperationClaims.Write },
                new() { Id = ++lastId, Name = TripsOperationClaims.Create },
                new() { Id = ++lastId, Name = TripsOperationClaims.Update },
                new() { Id = ++lastId, Name = TripsOperationClaims.Delete },
            ]
        );
        #endregion
        
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}
