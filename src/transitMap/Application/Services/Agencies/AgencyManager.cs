using Application.Features.Agencies.Rules;
using Application.Services.Repositories;
using Shared.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Agencies;

public class AgencyManager : IAgencyService
{
    private readonly IAgencyRepository _agencyRepository;
    private readonly AgencyBusinessRules _agencyBusinessRules;

    public AgencyManager(IAgencyRepository agencyRepository, AgencyBusinessRules agencyBusinessRules)
    {
        _agencyRepository = agencyRepository;
        _agencyBusinessRules = agencyBusinessRules;
    }

    public async Task<Agency?> GetAsync(
        Expression<Func<Agency, bool>> predicate,
        Func<IQueryable<Agency>, IIncludableQueryable<Agency, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Agency? agency = await _agencyRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return agency;
    }

    public async Task<IPaginate<Agency>?> GetListAsync(
        Expression<Func<Agency, bool>>? predicate = null,
        Func<IQueryable<Agency>, IOrderedQueryable<Agency>>? orderBy = null,
        Func<IQueryable<Agency>, IIncludableQueryable<Agency, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Agency> agencyList = await _agencyRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return agencyList;
    }

    public async Task<Agency> AddAsync(Agency agency)
    {
        Agency addedAgency = await _agencyRepository.AddAsync(agency);

        return addedAgency;
    }

    public async Task<Agency> UpdateAsync(Agency agency)
    {
        Agency updatedAgency = await _agencyRepository.UpdateAsync(agency);

        return updatedAgency;
    }

    public async Task<Agency> DeleteAsync(Agency agency, bool permanent = false)
    {
        Agency deletedAgency = await _agencyRepository.DeleteAsync(agency);

        return deletedAgency;
    }
}
