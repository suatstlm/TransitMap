using Application.Features.Routes.Commands.Create;
using Application.Features.Routes.Commands.Delete;
using Application.Features.Routes.Commands.Update;
using Application.Features.Routes.Queries.GetById;
using Application.Features.Routes.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Routes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateRouteCommand, Route>();
        CreateMap<Route, CreatedRouteResponse>();

        CreateMap<UpdateRouteCommand, Route>();
        CreateMap<Route, UpdatedRouteResponse>();

        CreateMap<DeleteRouteCommand, Route>();
        CreateMap<Route, DeletedRouteResponse>();

        CreateMap<Route, GetByIdRouteResponse>();

        CreateMap<Route, GetListRouteListItemDto>();
        CreateMap<IPaginate<Route>, GetListResponse<GetListRouteListItemDto>>();
    }
}