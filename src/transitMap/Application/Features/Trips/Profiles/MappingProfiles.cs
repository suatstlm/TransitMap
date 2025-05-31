using Application.Features.Trips.Commands.Create;
using Application.Features.Trips.Commands.Delete;
using Application.Features.Trips.Commands.Update;
using Application.Features.Trips.Queries.GetById;
using Application.Features.Trips.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Trips.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateTripCommand, Trip>();
        CreateMap<Trip, CreatedTripResponse>();

        CreateMap<UpdateTripCommand, Trip>();
        CreateMap<Trip, UpdatedTripResponse>();

        CreateMap<DeleteTripCommand, Trip>();
        CreateMap<Trip, DeletedTripResponse>();

        CreateMap<Trip, GetByIdTripResponse>();

        CreateMap<Trip, GetListTripListItemDto>();
        CreateMap<IPaginate<Trip>, GetListResponse<GetListTripListItemDto>>();
    }
}