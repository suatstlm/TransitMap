using Application.Features.StopTimes.Commands.Create;
using Application.Features.StopTimes.Commands.Delete;
using Application.Features.StopTimes.Commands.Update;
using Application.Features.StopTimes.Queries.GetById;
using Application.Features.StopTimes.Queries.GetList;
using AutoMapper;
using Shared.Application.Responses;
using Domain.Entities;
using Shared.Persistence.Paging;

namespace Application.Features.StopTimes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateStopTimeCommand, StopTime>();
        CreateMap<StopTime, CreatedStopTimeResponse>();

        CreateMap<UpdateStopTimeCommand, StopTime>();
        CreateMap<StopTime, UpdatedStopTimeResponse>();

        CreateMap<DeleteStopTimeCommand, StopTime>();
        CreateMap<StopTime, DeletedStopTimeResponse>();

        CreateMap<StopTime, GetByIdStopTimeResponse>();

        CreateMap<StopTime, GetListStopTimeListItemDto>();
        CreateMap<IPaginate<StopTime>, GetListResponse<GetListStopTimeListItemDto>>();
    }
}