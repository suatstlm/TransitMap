using Application.Features.ServiceCalendars.Commands.Create;
using Application.Features.ServiceCalendars.Commands.Delete;
using Application.Features.ServiceCalendars.Commands.Update;
using Application.Features.ServiceCalendars.Queries.GetById;
using Application.Features.ServiceCalendars.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ServiceCalendars.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateServiceCalendarCommand, ServiceCalendar>();
        CreateMap<ServiceCalendar, CreatedServiceCalendarResponse>();

        CreateMap<UpdateServiceCalendarCommand, ServiceCalendar>();
        CreateMap<ServiceCalendar, UpdatedServiceCalendarResponse>();

        CreateMap<DeleteServiceCalendarCommand, ServiceCalendar>();
        CreateMap<ServiceCalendar, DeletedServiceCalendarResponse>();

        CreateMap<ServiceCalendar, GetByIdServiceCalendarResponse>();

        CreateMap<ServiceCalendar, GetListServiceCalendarListItemDto>();
        CreateMap<IPaginate<ServiceCalendar>, GetListResponse<GetListServiceCalendarListItemDto>>();
    }
}