using Application.Features.Stops.Commands.Create;
using Application.Features.Stops.Commands.Delete;
using Application.Features.Stops.Commands.Update;
using Application.Features.Stops.Queries.GetById;
using Application.Features.Stops.Queries.GetList;
using AutoMapper;
using Shared.Application.Responses;
using Domain.Entities;
using Shared.Persistence.Paging;

namespace Application.Features.Stops.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateStopCommand, Stop>();
        CreateMap<Stop, CreatedStopResponse>();

        CreateMap<UpdateStopCommand, Stop>();
        CreateMap<Stop, UpdatedStopResponse>();

        CreateMap<DeleteStopCommand, Stop>();
        CreateMap<Stop, DeletedStopResponse>();

        CreateMap<Stop, GetByIdStopResponse>();

        CreateMap<Stop, GetListStopListItemDto>();
        CreateMap<IPaginate<Stop>, GetListResponse<GetListStopListItemDto>>();
    }
}