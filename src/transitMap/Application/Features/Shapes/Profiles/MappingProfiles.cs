using Application.Features.Shapes.Commands.Create;
using Application.Features.Shapes.Commands.Delete;
using Application.Features.Shapes.Commands.Update;
using Application.Features.Shapes.Queries.GetById;
using Application.Features.Shapes.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Shapes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateShapeCommand, Shape>();
        CreateMap<Shape, CreatedShapeResponse>();

        CreateMap<UpdateShapeCommand, Shape>();
        CreateMap<Shape, UpdatedShapeResponse>();

        CreateMap<DeleteShapeCommand, Shape>();
        CreateMap<Shape, DeletedShapeResponse>();

        CreateMap<Shape, GetByIdShapeResponse>();

        CreateMap<Shape, GetListShapeListItemDto>();
        CreateMap<IPaginate<Shape>, GetListResponse<GetListShapeListItemDto>>();
    }
}