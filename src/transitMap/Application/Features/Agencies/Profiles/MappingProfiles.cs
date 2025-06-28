using Application.Features.Agencies.Commands.Create;
using Application.Features.Agencies.Commands.Delete;
using Application.Features.Agencies.Commands.Update;
using Application.Features.Agencies.Queries.GetById;
using Application.Features.Agencies.Queries.GetList;
using AutoMapper;
using Shared.Application.Responses;
using Domain.Entities;
using Shared.Persistence.Paging;

namespace Application.Features.Agencies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateAgencyCommand, Agency>();
        CreateMap<Agency, CreatedAgencyResponse>();

        CreateMap<UpdateAgencyCommand, Agency>();
        CreateMap<Agency, UpdatedAgencyResponse>();

        CreateMap<DeleteAgencyCommand, Agency>();
        CreateMap<Agency, DeletedAgencyResponse>();

        CreateMap<Agency, GetByIdAgencyResponse>();

        CreateMap<Agency, GetListAgencyListItemDto>();
        CreateMap<IPaginate<Agency>, GetListResponse<GetListAgencyListItemDto>>();
    }
}